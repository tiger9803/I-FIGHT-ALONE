using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

	public Transform grassPrefab;
    public Transform waterPrefab;
    public Transform tree1Prefab;
    public Transform tree2Prefab;
	public Vector2 mapSize;

	void Start() {
		GenerateMap();
        GenerateTree();
	}

	void GenerateMap() 
	{
		string holderName = "Generated Map";
		if (transform.Find(holderName)) {
			DestroyImmediate(transform.Find(holderName).gameObject);
		}

		Transform mapHolder = new GameObject(holderName).transform;
		mapHolder.parent = transform;

		for (int x = 0; x < mapSize.x; x ++) {
			for (int y = 0; y < mapSize.y; y ++) {
				Vector3 tilePosition = new Vector3(-mapSize.x/2 + 0.5f + x, 0, -mapSize.y/2 + 0.5f + y);
				
				int random_water = Random.Range(1, 100);
				Transform type = grassPrefab;
				if(random_water <= 2){type = waterPrefab;}

				Transform newTile = Instantiate(type, tilePosition, Quaternion.Euler(new Vector3(0,0,0))) as Transform;
				newTile.parent = mapHolder;
			}
		}
	}

    void GenerateTree()
    {
        string holderName = "Generated Tree";
		if (transform.Find(holderName)) {
			DestroyImmediate(transform.Find(holderName).gameObject);
		}

		Transform mapHolder = new GameObject(holderName).transform;
		mapHolder.parent = transform;

        for(int i = 0; i < (mapSize.x*mapSize.y/40); i++)
        {
            Vector3 tilePosition = new Vector3(Random.Range(-mapSize.x/2+0.5f, mapSize.x/2-0.5f), 0, Random.Range(-mapSize.y/2+0.5f, mapSize.y/2-0.5f));
            int type = Random.Range(0,2);
            Transform newTile;
            if(type == 0)
            {
                newTile = Instantiate(tree1Prefab, tilePosition, Quaternion.Euler(new Vector3(0,0,0))) as Transform;    
            }
            else
            {
                newTile = Instantiate(tree2Prefab, tilePosition, Quaternion.Euler(new Vector3(0,0,0))) as Transform;    
            }
            newTile.localScale = Vector3.one * 0.3f;
            newTile.parent = mapHolder;
        }
        
    }
}