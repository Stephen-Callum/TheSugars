using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SugarRush_GameMode : MonoBehaviour
{   
    [SerializeField]
    private Text _scoreBox;
    //[SerializeField]
    //private Canvas _UICanvas;
    private int _playerScore;
    //private double _timerText;
    [SerializeField]
    private GameObject _timerDisplay01;
    [SerializeField]
    private GameObject _timerDisplay02;
    private bool _isTakingSecond;
    private int _roundTime;

    public Text ScoreBox { get => _scoreBox; set => _scoreBox = value; }
    public int PlayerScore { get => _playerScore; set => _playerScore = value; }
    //public double Timer { get => _timerText; set => _timerText = value; }
    //public Canvas UICanvas { get => _UICanvas; set => _UICanvas = value; }
    public GameObject Timer02 { get => _timerDisplay02; set => _timerDisplay02 = value; }
    public GameObject Timer01 { get => _timerDisplay01; set => _timerDisplay01 = value; }
    public int RoundTime { get => _roundTime; set => _roundTime = value; }

    private void DecrementGameTimer()
    {
        //Timer -= Time.deltaTime;
        //if (Timer >= 0)
        //{
        //    UICanvas.transform.GetChild(2).GetComponent<Text>().text = Timer.ToString();
        //    UICanvas.transform.GetChild(1).GetComponent<Text>().text = Timer.ToString();
        //}
        //if (Timer <= 0)
        //{
        //    UICanvas.transform.GetChild(1).GetComponent<Text>().text = "0";
        //    UICanvas.transform.GetChild(2).GetComponent<Text>().text = "0";
        //}
    }

    private void Awake()
    {
        PlayerScore = 0;
        ScoreBox.text = _playerScore.ToString();
        _isTakingSecond = false;
        RoundTime = 10;
        Timer01.GetComponent<Text>().text = RoundTime.ToString();
        Timer02.GetComponent<Text>().text = RoundTime.ToString();
        //Timer = 5.0f;
        //// Timer01 & Timer02
        //UICanvas.transform.GetChild(2).GetComponent<Text>().text = Timer.ToString("00");
        //UICanvas.transform.GetChild(1).GetComponent<Text>().text = Timer.ToString("00");
    }

    private void Update()
    {
        //DecrementGameTimer();
        if (_isTakingSecond == false)
        {
            StartCoroutine(TakeSecond());
        }
    }

    IEnumerator TakeSecond()
    {
        if(RoundTime != 0)
        {
            _isTakingSecond = true;
            RoundTime -= 1;
            Timer01.GetComponent<Text>().text = RoundTime.ToString();
            Timer02.GetComponent<Text>().text = RoundTime.ToString();
            yield return new WaitForSeconds(1);
            _isTakingSecond = false;
        }
    }

    public void IncrementScore(Base_PickUp pickup)
    {
        PlayerScore += pickup.GetValue();
        ScoreBox.text = _playerScore.ToString();
    }
}
