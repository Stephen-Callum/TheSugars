using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static int redirectToLevel = 1;

    void Update()
    {
        if (redirectToLevel == 1)
        {
            SceneManager.LoadScene(redirectToLevel);
        }
    }
}
