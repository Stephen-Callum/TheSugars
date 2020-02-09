using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _score;
    [SerializeField]
    private GameObject _scoreName;
    [SerializeField]
    private GameObject _rank;

    public GameObject Rank { get => _rank; set => _rank = value; }
    public GameObject ScoreName { get => _scoreName; set => _scoreName = value; }
    public GameObject Score { get => _score; set => _score = value; }

    public void SetScore(string rank, string name, string score)
    {
        Rank.GetComponent<Text>().text = rank;
        ScoreName.GetComponent<Text>().text = name;
        Score.GetComponent<Text>().text = score;
    }
}
