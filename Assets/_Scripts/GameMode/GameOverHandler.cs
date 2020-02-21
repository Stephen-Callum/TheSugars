using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    private static int _playerScore;
    //private SugarRush_GameMode _sugarRushGameMode;
    private TextMeshProUGUI _gameOverScore;
    public static int PlayerScore { get => _playerScore; set => _playerScore = value; }
    public TextMeshProUGUI GameOverScore { get => _gameOverScore; set => _gameOverScore = value; }

    private void Awake()
    {
        GameOverScore = GetComponent<TextMeshProUGUI>();
        //_sugarRushGameMode = GetComponent<SugarRush_GameMode>();
        GameOverScore.text += $" {SugarRush_GameMode.PlayerScore.ToString()}";
    }


}
