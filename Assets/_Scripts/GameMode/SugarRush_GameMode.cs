using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SugarRush_GameMode : MonoBehaviour
{   
    [SerializeField]
    private Text _scoreBox;

    private int _playerScore;

    public Text ScoreBox { get => _scoreBox; set => _scoreBox = value; }
    public int PlayerScore { get => _playerScore; set => _playerScore = value; }

    private void Awake()
    {
        _playerScore = 0;
        ScoreBox.text = _playerScore.ToString();
    }

    public void IncrementScore(Base_PickUp pickup)
    {
        print(pickup.GetValue());
        _playerScore += pickup.GetValue();
        ScoreBox.text = _playerScore.ToString();
    }
}
