﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SugarRush_GameMode : MonoBehaviour
{   
    [SerializeField]
    private Text _scoreBox;
    private static int _playerScore;
    [SerializeField]
    private GameObject _timerDisplay01;
    [SerializeField]
    private GameObject _timerDisplay02;
    private bool _isTakingSecond;
    [SerializeField]
    private int _roundTime;
    private UnityEvent m_GameEnded;
    private static bool _gamePaused;

    public Text ScoreBox { get => _scoreBox; set => _scoreBox = value; }
    public static int PlayerScore { get => _playerScore; set => _playerScore = value; }
    public GameObject Timer02 { get => _timerDisplay02; set => _timerDisplay02 = value; }
    public GameObject Timer01 { get => _timerDisplay01; set => _timerDisplay01 = value; }
    public int RoundTime { get => _roundTime; set => _roundTime = value; }
    public static bool GamePaused { get => _gamePaused; set => _gamePaused = value; }
    public GameObject PlayerObject;

    // Ran before the first frame
    private void Awake()
    {
        PlayerScore = 0;
        ScoreBox.text = _playerScore.ToString();
        _isTakingSecond = false;
        Timer01.GetComponent<Text>().text = RoundTime.ToString();
        Timer02.GetComponent<Text>().text = RoundTime.ToString();

        if (m_GameEnded == null)
        {
            m_GameEnded = new UnityEvent();
            m_GameEnded.AddListener(LevelManager.TimeUpEndGame);
        }
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        GamePaused = false;
    }

    // Run every frame
    private void Update()
    {
        if (_isTakingSecond == false)
        {
            StartCoroutine(TakeSecond());
        }
        // End game if timer runs down to zero.
        if (RoundTime == 0)
        {
            print("Times UP!");
            m_GameEnded.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GamePaused)
            {
                //Activate GUI
                //Pause Game with Time.timescale=0; stuff
                print("Pause button pressed");
                Time.timeScale = 1;
                GamePaused = false;
            }
            else
            {
                //Play Game with time.timescale=1; stuff
                //Deactivate GUI
                Time.timeScale = 0;
                GamePaused = true;
            }
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

    // Increment score based on pickup value
    public void IncrementScore(Base_PickUp pickup)
    {
        PlayerScore += pickup.GetValue();
        ScoreBox.text = _playerScore.ToString();
    }
}
