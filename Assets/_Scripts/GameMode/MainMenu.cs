using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start the game at level 1
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    

    // Temporary duplication code for fix
    public void GoToLeaderBoardFromMM()
    {
        LevelManager.LastActiveScene = "MainMenu";
        SceneManager.LoadScene("LeaderBoardScene");
    }

    // Back to main menu from leaderboard
    public void BackToMainMenuFromLB()
    {
        LevelManager.LastActiveScene = "LeaderBoardScene";
        SceneManager.LoadScene("MainMenu");
    }
}
