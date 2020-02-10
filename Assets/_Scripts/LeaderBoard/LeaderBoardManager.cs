using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class LeaderBoardManager : MonoBehaviour
{
    private string _connectionString;
    private List<LeaderBoard> LeaderBoard = new List<LeaderBoard>();
    [SerializeField]
    private GameObject _scorePrefab;
    [SerializeField]
    private Transform _scoreParent;
    public GameObject ScorePrefab { get => _scorePrefab; set => _scorePrefab = value; }
    public Transform ScoreParent { get => _scoreParent; set => _scoreParent = value; }

    // Start is called before the first frame update
    void Start()
    {
        _connectionString = $"URI=file:{Application.persistentDataPath}/LeaderBoardDB.db";
        CreateLeaderBoard();
        ShowScores();
    }

    private void CreateLeaderBoard()
    {
        using (var dbConnection = new SqliteConnection(_connectionString))
        {
            dbConnection.Open();
            //var dropCommand = dbConnection.CreateCommand();
            //dropCommand.CommandText = "DROP TABLE IF EXISTS LeaderBoard";

            var createCommand = dbConnection.CreateCommand();
            createCommand.CommandText = "CREATE TABLE IF NOT EXISTS LeaderBoard (" +
                "PlayerID INTEGER " +
                "PRIMARY KEY AUTOINCREMENT NOT NULL " +
                "UNIQUE," +
                "Name     TEXT NOT NULL," +
                "Score INTEGER  NOT NULL," +
                "Date     DATETIME CONSTRAINT[CURRENT_DATE] DEFAULT(CURRENT_DATE)" +
                "); ";
            //dropCommand.ExecuteReader();
            createCommand.ExecuteReader();
            dbConnection.Close();
        }
    }

    private void InsertScore(string name, int newScore)
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

    private void InsertIntoLeaderBoard()
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
                "('Stephens', '999')";
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
    }

    private void ShowScores()
    {
        GetScores();
        for (int i = 0; i < LeaderBoard.Count; i++)
        {
            GameObject tmpObj = Instantiate(ScorePrefab);

            LeaderBoard tmpScore = LeaderBoard[i];

            tmpObj.GetComponent<LeaderBoardScript>().SetScore("#" + (i+1).ToString(), tmpScore.Name, tmpScore.Score.ToString());

            tmpObj.transform.SetParent(ScoreParent);
        }
    }

}
