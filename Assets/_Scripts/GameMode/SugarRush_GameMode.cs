using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SugarRush_GameMode : MonoBehaviour
{
    private GameObject _scoreBox;

    public GameObject ScoreBox { get => _scoreBox; set => _scoreBox = value; }

    private void Awake()
    {
        ScoreBox = GameObject.FindGameObjectWithTag("ScoreText");
    }
}
