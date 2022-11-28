using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunningTime : MonoBehaviour
{
    float sec;
    int min;

    [SerializeField]
    Text timerText;

    private void Update()
    {
        Timer();
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
}
