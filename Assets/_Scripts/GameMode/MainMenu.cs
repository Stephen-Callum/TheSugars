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

    public void GoToLeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoardScene");
    }

    public void BackToMainMenuFromLB()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
