using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // access modifier, temporary fix.
    public static string _lastActiveScene;

    public static string LastActiveScene { get => _lastActiveScene; set => _lastActiveScene = value; }

    // Used to check what the last active scene was.
    public static void SetLastActiveScene()
    {
        LastActiveScene = SceneManager.GetActiveScene().name;
    }

    // Set the Last active scene and then go to the next scene.
    public void GoToNextLevel()
    {
        SetLastActiveScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //public static void GoToLeaderBoard()
    //{
    //    SceneManager.LoadScene("LeaderBoardScene");
    //}

    // Handle what happens when the game timer runs to zero.
    public static void TimeUpEndGame()
    {
        print("Level Manager being used");
        // Temporary fix
        SceneManager.LoadScene("GameOverScene");
    }

    private void Update()
    {
        if (!SceneManager.GetActiveScene().name.Equals("LeaderBoardScene"))
        {
            print("This is not the leaderboard scene.");
        }
    }
}
