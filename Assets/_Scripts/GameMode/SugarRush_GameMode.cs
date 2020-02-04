using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SugarRush_GameMode : MonoBehaviour
{   
    [SerializeField]
    private Text _scoreBox;
    [SerializeField]
    private Canvas _UICanvas;
    private int _playerScore;
    private double _timerText;

    public Text ScoreBox { get => _scoreBox; set => _scoreBox = value; }
    public int PlayerScore { get => _playerScore; set => _playerScore = value; }
    public double Timer { get => _timerText; set => _timerText = value; }
    public Canvas UICanvas { get => _UICanvas; set => _UICanvas = value; }

    private void DecrementGameTimer()
    {
        if (Timer >= 0)
        {
            Timer -= Time.deltaTime;
            UICanvas.transform.GetChild(2).GetComponent<Text>().text = Timer.ToString();
            UICanvas.transform.GetChild(1).GetComponent<Text>().text = Timer.ToString();
        }
        
    }

    private void Awake()
    {
        PlayerScore = 0;
        ScoreBox.text = _playerScore.ToString();
        Timer = 5.0f;
        UICanvas.transform.GetChild(2).GetComponent<Text>().text = Timer.ToString("00");
        UICanvas.transform.GetChild(1).GetComponent<Text>().text = Timer.ToString("00");
    }

    private void Update()
    {
        DecrementGameTimer();
    }

    public void IncrementScore(Base_PickUp pickup)
    {
        PlayerScore += pickup.GetValue();
        ScoreBox.text = _playerScore.ToString();
    }
}
