using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level01");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToLeaderBoardFromGO()
    {
        // Temporary Fix
        LevelManager.LastActiveScene = "GameOverScene";
        SceneManager.LoadScene("LeaderBoardScene");
    }

    // Temporary duplication code for fix
    public void GoToLeaderBoardFromMM()
    {
        // Temporary Fix
        LevelManager.LastActiveScene = "MainMenu";
        SceneManager.LoadScene("LeaderBoardScene");
    }

    public void BackToMainMenuFromLB()
    {
        // Temporary fix
        LevelManager.LastActiveScene = "LeaderBoardScene";
        SceneManager.LoadScene("MainMenu");
    }
}
