using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectUpgrade : MonoBehaviour
{
    public GameObject[] btn;
    GameObject running;

    void Start()
    {
        running = GetComponent<RunningTime>().selectUpgrade;
    }

    public void Click_Upgrade()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        switch (clickObject.name)
        {
            case "upgrade1":
                GetComponent<GameDirector>().select_P.GetComponent<PlayerController>().maxHp += 50;
                GetComponent<RunningTime>().ChangeHp();
                break;

            case "upgrade2":
                GetComponent<GameDirector>().select_P.GetComponent<PlayerController>().speed += 1.0f;
                break;

            case "upgrade3":
                break;
        }

        running.SetActive(false);
        Time.timeScale = 1;
    }
    
}
