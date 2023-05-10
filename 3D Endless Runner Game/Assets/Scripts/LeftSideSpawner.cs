using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSideSpawner : MonoBehaviour
{
    
    public GameObject stupa;
    int spawnObjectEveryNTiles = 15;
    int tilesCount = 0;

    GroundSpawner groundSpawner; 

    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        tilesCount = groundSpawner.GetTilesSpawnedCount();
        SpawnStupa();
    }

    public void SpawnStupa()
    {
        int obstacleSpawnIndex = 2;
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        if (tilesCount == 0)
        {
            Instantiate(stupa, spawnPoint.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
            tilesCount++;
            return;
        }
        if ((tilesCount - 1) % spawnObjectEveryNTiles == 0)
        {
            Instantiate(stupa, spawnPoint.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
        }
        tilesCount++;

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
