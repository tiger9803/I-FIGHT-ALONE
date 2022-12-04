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
    float maxHp;
    bool flag = true;

    int playerNum;
    public GameObject hpGauge;
    public GameObject expGauge;
    public GameObject selectUpgrade;
    public GameObject[] player;

    [SerializeField]
    Text timerText, scoreText, levelText, hp_status, exp_status;

    void Start()
    {   
        playerNum = GameManager.instance.selectPlayer;
        timerText.text = "Time 00:00";
        scoreText.text = "0000";
        maxHp = player[playerNum].GetComponent<PlayerController>().maxHp; // hp 초기화
        hp_status.text = player[playerNum].GetComponent<PlayerController>().hp.ToString() + " / " + maxHp.ToString();
        exp_status.text = player[playerNum].GetComponent<PlayerController>().exp.ToString() + " / " + "100";
        ChangeExp(); // exp 초기화
    }

    private void Update()
    {   
        if((int)sec == 10 && flag == true)
        {
            StartCoroutine("TimeScore", 10);
            flag = false;
        }

        Timer();

        if(player[playerNum].GetComponent<PlayerController>().hp <= 0)
        {
            GameManager.instance.time = timerText.text;
            GameManager.instance.score = score;
            GameManager.instance.accScore += int.Parse(scoreText.text);
            Debug.Log("total : " + GameManager.instance.accScore); // debug
            SceneManager.LoadScene("GameEndScene");
        }
    }
    
    // delayTime 마다 AddScore(0) 호출
    IEnumerator TimeScore(float delayTime)
    {   
        AddScore(0);
        yield return new WaitForSeconds(delayTime);
        StartCoroutine("TimeScore", delayTime);
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

    public void AddScore(int type)
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

        scoreText.text = string.Format("{0:0000}", score);
    }

    public void ChangeHp()
    {   
        maxHp = player[playerNum].GetComponent<PlayerController>().maxHp;
        float now_Hp = player[playerNum].GetComponent<PlayerController>().hp;
        hpGauge.GetComponent<Image>().fillAmount = now_Hp / maxHp;
        hp_status.text = player[playerNum].GetComponent<PlayerController>().hp.ToString() + " / " + maxHp.ToString();
    }

    public void ChangeExp()
    {
        float now_Exp = player[playerNum].GetComponent<PlayerController>().exp;
        expGauge.GetComponent<Image>().fillAmount = now_Exp / 100.0f;
        exp_status.text = player[playerNum].GetComponent<PlayerController>().exp.ToString() + " / " + "100";
    }

    public void ChangeLevel()
    {
        levelText.text = "LV" + player[playerNum].GetComponent<PlayerController>().level;
    }
}
