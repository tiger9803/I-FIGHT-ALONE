using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPooling : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 0.5f;
    public int maxSpawnCnt = 50;
    int spawnCnt = 1;

    int poolSize;
    float deltaSpawnTime;
    GameObject[] enemyPool;

    GameObject mobHolder;

    void Start()
    {
        string holderName = "Generated Mob";
        if(transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        mobHolder = new GameObject(holderName);
        mobHolder.transform.parent = transform;

        deltaSpawnTime = 0.0f;
        poolSize = maxSpawnCnt;

        enemyPool = new GameObject[poolSize];
        for(int i = 0; i<poolSize; i++)
        {
            enemyPool[i] = Instantiate(enemy) as GameObject;
            enemyPool[i].transform.parent = mobHolder.transform;
            enemyPool[i].name = string.Format("Enemy_{0}", i+1);
            enemyPool[i].SetActive(false);
        }
    }

    void Update()
    {
        if(spawnCnt > maxSpawnCnt) return;
        deltaSpawnTime += Time.deltaTime;

        if(deltaSpawnTime > spawnTime)
        {
            deltaSpawnTime = 0.0f;
            for(int i = 0; i<poolSize-1; ++i)
            {
                GameObject enemyObj1 = enemyPool[i];
                GameObject enemyObj2 = enemyPool[i+1];

                if(enemyObj1.activeSelf == true) continue;
                if(enemyObj2.activeSelf == true) continue;

                float x = Random.Range(-5.0f, 5.0f);
                enemyObj1.transform.position = new Vector3(x, 0f, 3.0f);
                enemyObj2.transform.position = new Vector3(x, 0f, 3.0f);

                enemyObj1.SetActive(true);
                enemyObj2.SetActive(true);
                break;
            }
        }
    }
}
