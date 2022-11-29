using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenerator : MonoBehaviour
{
    public Transform enemyA;
    public Transform target;

    Transform mobHolder;
    float delta = 0;
    float span = 0.5f;
    
    void Start()
    {   
        string holderName = "Generated Mob";
        if(transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        mobHolder = new GameObject(holderName).transform;
        mobHolder.parent = transform;

        
    }

    void Update()
    {   
        delta += Time.deltaTime;
        if(this.delta >= span)
        {
            this.delta = 0;
            SpawnMob();
        }
    }

    void SpawnMob()
    {
        Vector3 targetPos = target.position;
        if(targetPos.x > 0){targetPos.x -= 5;}
        else{targetPos.x += 5;}
        if(targetPos.z > 0){targetPos.z -= 5;}
        else{targetPos.z += 5;}

        Transform go;
        go = Instantiate(this.enemyA) as Transform;
        go.transform.position = targetPos;
        go.parent = mobHolder;
    }

    
}
