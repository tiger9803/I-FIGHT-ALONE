using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform[] target;
    public Vector3 offset;
    Transform player;
    int playerNum;

    void Start()
    {
        playerNum = GameManager.instance.selectPlayer;
        player = target[playerNum];
    }

    void Update()
    {
        transform.position = player.position + offset;
    }
}
