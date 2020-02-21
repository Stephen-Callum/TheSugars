using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static int redirectToLevel = 1;
    private static string _lastActiveScene;

    public static string LastActiveScene { get => _lastActiveScene; set => _lastActiveScene = value; }


    //void Update()
    //{
    //    if (redirectToLevel == 1)
    //    {
    //        SceneManager.LoadScene(redirectToLevel);
    //    }
    //}

    public static void SetLastActiveScene()
    {
        LastActiveScene = SceneManager.GetActiveScene().name;
    }

    public static void GoToLeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoardScene");
    }

    // Handle what happens when the game timer runs to zero
    public static void TimeUpEndGame()
    {
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
