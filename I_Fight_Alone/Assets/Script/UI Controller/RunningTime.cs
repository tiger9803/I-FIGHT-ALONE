using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RunningTime : MonoBehaviour
{
    float sec;
    int min;
    int score;

    [SerializeField]
    Text timerText, scoreText;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        timerText.text = "Time 00:00";
        scoreText.text = "0000";
    }

    private void Update()
    {
        Timer();

        if(player.GetComponent<PlayerController>().hp <= 0)
        {
            GameManager.instance.time = "Time " + timerText.text;
            SceneManager.LoadScene("GameEndScene");
        }
    }

    void Timer()
    {
        sec += Time.deltaTime;

        timerText.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);

        if((int)sec > 59)
        {
            sec = 0;
            min++;
        }
    }

    void AddScore(int type)
    {   
        if(type == 0) // time score
        {
            score += 50;
        }
        else if(type == 1) // mob 1
        {
            score += 10;
        }
        else if(type == 2) // mob 2
        {
            score += 20;
        }
        else if(type == 3) // mob 3
        {
            score += 30;
        }
        else // boss
        {
            score += 1000;
        }
    }
}
