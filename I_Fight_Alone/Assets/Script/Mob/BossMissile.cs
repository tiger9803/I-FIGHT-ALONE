using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMissile : MobBullet
{
    GameObject player;
    int playerNum;
    public Transform target;
    NavMeshAgent nav;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("GameDirector");
        target = player.GetComponent<GameDirector>().select_P.transform;
    }


    void Update()
    {
        // nav.SetDestination(target.position);
    }
}
