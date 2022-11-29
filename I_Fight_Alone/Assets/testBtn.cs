using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class testBtn : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }
    public void Click()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        player.GetComponent<PlayerController>().hp = 0;
    }
}
