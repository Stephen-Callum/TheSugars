using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static string _lastActiveScene;

    public static string LastActiveScene { get => _lastActiveScene; set => _lastActiveScene = value; }

    // Used to set what the last scene was. Used to check
    public static void SetLastActiveScene()
    {
        LastActiveScene = SceneManager.GetActiveScene().name;
    }

    // Set the Last active scene and then go to the next level.
    public void GoToNextLevel()
    {
        SetLastActiveScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Handle what happens when the game timer runs to zero.
    public static void TimeUpEndGame()
    {
        print("Level Manager being used");
        SceneManager.LoadScene("GameOverScene");
    }

    // Go to leaderboard from game over scene
    public void GoToLeaderBoardFromGO()
    {
        // Temporary Fix
        LastActiveScene = "GameOverScene";
        SceneManager.LoadScene("LeaderBoardScene");
    }

    // Back to main menu from leaderboard
    public void BackToMainMenuFromLB()
    {
        LastActiveScene = "LeaderBoardScene";
        SceneManager.LoadScene("MainMenu");
    }
}
