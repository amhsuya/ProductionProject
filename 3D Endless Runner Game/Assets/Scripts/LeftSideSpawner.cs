using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSideSpawner : MonoBehaviour
{
    
    public GameObject stupa;
    int spawnObjectEveryNTiles = 23;
    int tilesCount = 0;

    public GameObject rath;

    private bool spawnDharahara = true;

    GroundSpawner groundSpawner;

    private GameObject currentObstacle;

    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        tilesCount = groundSpawner.GetTilesSpawnedCount();
        
        SpawnRath();
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
    public void SpawnRath()
    {
        int obstacleSpawnIndex = 4;
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        if (tilesCount == 0)
        {
            Instantiate(rath, spawnPoint.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
            tilesCount++;
            return;
        }
        if ((tilesCount - 1) % spawnObjectEveryNTiles == 0)
        {
            Instantiate(rath, spawnPoint.position + new Vector3(-6.6f, 1, 0), Quaternion.Euler(0, 0, -90), transform);
        }
        tilesCount++;
    }
    public void ResetTilesCount()
    {
        tilesCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
