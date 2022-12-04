using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPooling : MonoBehaviour
{
    public GameObject[] enemy;
    public Transform spawnPosition;
    public int mobIndex = 0;
    public float spawnTime;
    public float checkTime;
    public GameObject mobHolder;

    void Start()
    {
        SpawnMob();
    }

    void Update()
    {
        checkTime += Time.deltaTime;
        CheckTime();
    }

    void SpawnMob()
    {
        StartCoroutine(CoSpawnMob());
    }

    IEnumerator CoSpawnMob()
    {
        while (true)
        {
            if (mobIndex == (int)Enemy.Type.D)
            {
                Instantiate(enemy[mobIndex], spawnPosition.position, Quaternion.identity, mobHolder.transform);
                break;
            }
            else
            {
                Instantiate(enemy[mobIndex], spawnPosition.position, Quaternion.identity, mobHolder.transform);
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void CheckTime()
    {
        switch (checkTime)
        {
            case >= 30:
                mobIndex = (int)Enemy.Type.D;
                break;
            case >= 20:
                mobIndex = (int)Enemy.Type.C;
                break;
            case >= 10:
                mobIndex = (int)Enemy.Type.B;
                break;
        }
    }
}
