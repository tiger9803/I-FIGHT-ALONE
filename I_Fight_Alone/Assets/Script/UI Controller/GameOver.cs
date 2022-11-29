using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{   
    public Text timerText;
    
    void Start()
    {   
        timerText.text = GameManager.instance.time;
    }

    void Update()
    {
        
    }
}
