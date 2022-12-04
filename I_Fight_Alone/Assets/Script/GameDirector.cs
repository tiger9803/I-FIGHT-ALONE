using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{   
    int playerNum;
    public GameObject player0;
    public GameObject player1;
    public GameObject player2;
    public GameObject select_P;

    void Start()
    {   
        playerNum = GameManager.instance.selectPlayer;

        if(playerNum == 0)
        {   
            select_P = player0;
            player0.SetActive(true);
            player1.SetActive(false);
            player2.SetActive(false);

        }
        else if(playerNum == 1)
        {
            select_P = player1;
            player0.SetActive(false);
            player1.SetActive(true);
            player2.SetActive(false);
        }
        else
        {   
            select_P = player2;
            player0.SetActive(false);
            player1.SetActive(false);
            player2.SetActive(true);
        }
    }
}
