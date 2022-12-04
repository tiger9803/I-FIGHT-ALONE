using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{   
    public Text timerText;
    public Text scoreText;
    
    void Start()
    {   
        timerText.text = "TIME" + GameManager.instance.time;
        scoreText.text = "SCORE " + GameManager.instance.score.ToString();

        GetComponent<Order>().ScoreSet("player", GameManager.instance.score, GameManager.instance.time);
    }
}