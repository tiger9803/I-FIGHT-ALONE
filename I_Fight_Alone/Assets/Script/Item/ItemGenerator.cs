using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public Transform[] itemPrefab;
    Transform itemHolder;
    GameObject map;
    Vector2 mapSize;
    float timer;
    int waitingTime;

    void Start()
    {
        timer = 0.0f;
        waitingTime = 2;
        // mapSize = new Vector2(100, 100); // MapGenerator 사용시 제거

        string holderName = "Generated Item";
        if(transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        itemHolder = new GameObject(holderName).transform;
        itemHolder.parent = transform;

        map = GameObject.Find("MapGenerator");  //MapGenerator 사용 시
        mapSize = map.GetComponent<MapGenerator>().mapSize; //MapGenerator 사용 시
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitingTime)
        {
            timer = 0;
            GenerateItem();
        }
    }

    void GenerateItem()
    {
        Vector3 itemPosition = new Vector3(Random.Range(-mapSize.x/2+2.0f, mapSize.x/2-2.0f), 0.5f, Random.Range(-mapSize.y/2+2.0f, mapSize.y/2-2.0f));
        
        int itemNum = Random.Range(0, 6); // Item 개수
        Transform newItem;

        if(itemNum == 0)
        {
            newItem = Instantiate(itemPrefab[0], itemPosition, Quaternion.Euler(new Vector3(0,0,0)));
        }
        else if(itemNum == 1)
        {
            newItem = Instantiate(itemPrefab[1], itemPosition, Quaternion.Euler(new Vector3(0,0,0)));
        }
        else if (itemNum == 2)
        {
            newItem = Instantiate(itemPrefab[2], itemPosition, Quaternion.Euler(new Vector3(0,0,0)));
        }
         else if (itemNum == 3)
        {
            newItem = Instantiate(itemPrefab[3], itemPosition, Quaternion.Euler(new Vector3(0,0,0)));
        }
         else if (itemNum == 4)
        {
            newItem = Instantiate(itemPrefab[4], itemPosition, Quaternion.Euler(new Vector3(0,0,0)));
        }
         else
        {
            newItem = Instantiate(itemPrefab[5], itemPosition, Quaternion.Euler(new Vector3(0,0,0)));
        }
        
        
        newItem.localScale = Vector3.one * 0.3f;
        newItem.parent = itemHolder;
    }
}
