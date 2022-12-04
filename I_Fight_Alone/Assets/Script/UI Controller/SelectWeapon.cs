using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{
    int accScore;
    int level;
    int maxLevel = 5;
    float maxGauge = 300.0f;
    public Text levelText;
    public GameObject accScoreGauge;
    public GameObject[] btn;
    
    void Start()
    {   
        if(GameManager.instance.accScore >= maxGauge && GameManager.instance.level < maxLevel)
        {
            GameManager.instance.level += 1;
            GameManager.instance.accScore -= (int)maxGauge;
        }

        levelText.text = "LV " + GameManager.instance.level.ToString();
        ChangeGauge();

        for(int i=btn.Length-1; i>=GameManager.instance.level; i--)
        {
            btn[i].GetComponent<Button>().interactable = false;
        }
    }

    void ChangeGauge()
    {
        accScoreGauge.GetComponent<Image>().fillAmount = GameManager.instance.accScore / maxGauge;
    }
}
