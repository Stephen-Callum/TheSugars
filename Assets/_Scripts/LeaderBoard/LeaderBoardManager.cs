using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class LeaderBoardManager : MonoBehaviour
{
    private string _connectionString;
    private List<LeaderBoard> LeaderBoard = new List<LeaderBoard>();
    [SerializeField]
    private GameObject _scorePrefab;
    [SerializeField]
    private Transform _scoreParent;
    [SerializeField]
    private int _topScoresToShow;
    [SerializeField]
    private int _scoresToSave;
    [SerializeField]
    private InputField _enterNameText;
    [SerializeField]
    private GameObject _nameDialog;
    private LevelManager _levelManager;
    public GameObject ScorePrefab { get => _scorePrefab; set => _scorePrefab = value; }
    public Transform ScoreParent { get => _scoreParent; set => _scoreParent = value; }
    public int TopScoresToShow { get => _topScoresToShow; set => _topScoresToShow = value; }
    public int ScoresToSave { get => _scoresToSave; set => _scoresToSave = value; }
    public GameObject NameDialog { get => _nameDialog; set => _nameDialog = value; }
    public InputField EnterNameText { get => _enterNameText; set => _enterNameText = value; }

    // Start is called before the first frame update
    void Start()
    {
        _connectionString = $"URI=file:{Application.persistentDataPath}/LeaderBoardDB.db";
        CreateLeaderBoard();
        //InsertIntoLeaderBoard("Test", 1000);
        DeleteExtraScores();
        ShowScores();
        print(LevelManager.LastActiveScene);
        if (LevelManager.LastActiveScene == "GameOverScene")
        {
            
            NameDialog.SetActive(true);
        }
    }

    // Create the first instance of the LeaderBoardDB if it does not exist.
    private void CreateLeaderBoard()
    {
        using (var dbConnection = new SqliteConnection(_connectionString))
        {
            dbConnection.Open();

            var createCommand = dbConnection.CreateCommand();
            createCommand.CommandText = "CREATE TABLE IF NOT EXISTS LeaderBoard (" +
                "PlayerID INTEGER " +
                "PRIMARY KEY AUTOINCREMENT NOT NULL " +
                "UNIQUE," +
                "Name     TEXT NOT NULL," +
                "Score INTEGER  NOT NULL," +
                "Date     DATETIME CONSTRAINT[CURRENT_DATE] DEFAULT(CURRENT_DATE)" +
                "); ";
            createCommand.ExecuteReader();
            dbConnection.Close();
        }
    }


    // Used to add the functionality of adding names to scoreboard from game.
    public void EnterName()
    {
        if (EnterNameText.text != string.Empty)
        {
            int score = SugarRush_GameMode.PlayerScore;
            InsertScore(EnterNameText.text, score);
            EnterNameText.text = string.Empty;
            NameDialog.SetActive(false);
            ShowScores();
        }
    }

    // Main method to insert scores
    private void InsertScore(string name, int newScore)
    {
        GetScores();
        int lbCount = LeaderBoard.Count;
        if (LeaderBoard.Count > 0)
        {
            LeaderBoard lowestScore = LeaderBoard[LeaderBoard.Count - 1];
            // if lowest score exists
            if (lowestScore != null && ScoresToSave > 0 && LeaderBoard.Count >= ScoresToSave && newScore > lowestScore.Score)
            {
                DeleteScore(lowestScore.ID);
                lbCount--;
            }
        }
        if (lbCount < ScoresToSave)
        {
            using (IDbConnection dbConnection = new SqliteConnection(_connectionString))
            {
                dbConnection.Open();

                using (IDbCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = $"INSERT INTO LeaderBoard(Name,Score) VALUES('{name}','{newScore}');";
                    dbCommand.ExecuteScalar();
                }

                dbConnection.Close();
            }
        }
    }

    // Delete score at row of specified ID
    private void DeleteScore(int id)
    {
        using (var dbConnection = new SqliteConnection(_connectionString))
        {
            dbConnection.Open();
            using (var dbCommand = dbConnection.CreateCommand())
            {
                dbCommand.CommandText = $"DELETE FROM LeaderBoard WHERE PlayerID = {id};";
                dbCommand.ExecuteScalar();
            }
            dbConnection.Close();
        }
    }

    // Test method for inserting scores.
    private void InsertIntoLeaderBoard(string name, int score)
    {
        using (var dbConnection = new SqliteConnection(_connectionString))
        {
            dbConnection.Open();
            var command = dbConnection.CreateCommand();
            command.CommandText = "INSERT INTO LeaderBoard (" +
                "Name," +
                "Score" +
                ")" +
                "VALUES" +
                $"('{name}', '{score}')";
            command.ExecuteNonQuery();
            dbConnection.Close();
        }
    }
    
    private void GetScores()
    {
        // Clear Leaderboard first
        LeaderBoard.Clear();

        using (var dbConnection = new SqliteConnection(_connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery  = "SELECT * FROM LeaderBoard";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LeaderBoard.Add(new LeaderBoard(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetDateTime(3)));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        LeaderBoard.Sort();
    }

    private void ShowScores()
    {
        GetScores();
        foreach (var score in GameObject.FindGameObjectsWithTag("Score"))
        {
            Destroy(score);
        }
        for (int i = 0; i < TopScoresToShow; i++)
        {
            if (i <= LeaderBoard.Count - 1)
            {
                GameObject tmpObj = Instantiate(ScorePrefab);

                LeaderBoard tmpScore = LeaderBoard[i];

                tmpObj.GetComponent<LeaderBoardScript>().SetScore("#" + (i + 1).ToString(), tmpScore.Name, tmpScore.Score.ToString());

                tmpObj.transform.SetParent(ScoreParent);
            }
        }
    }

    private void DeleteExtraScores()
    {
        GetScores();

        if (ScoresToSave < LeaderBoard.Count)
        {
            print($"DeleteExtraScores BEING RUN... ScoresToSave:{ScoresToSave}... Leaderboard.Count{LeaderBoard.Count}");
            int deleteCount = LeaderBoard.Count - ScoresToSave;
            LeaderBoard.Reverse();

            using (var dbConnection = new SqliteConnection(_connectionString))
            {
                dbConnection.Open();
                using (var dbCommand = dbConnection.CreateCommand())
                {
                    for (int i = 0; i < deleteCount; i++)
                    {
                        dbCommand.CommandText = $"DELETE FROM LeaderBoard WHERE PlayerID = {LeaderBoard[i].ID};";
                        dbCommand.ExecuteScalar();
                    }
                }
                dbConnection.Close();
            }
        }
    }

}
