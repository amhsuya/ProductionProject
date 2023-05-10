using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSideSpawner1 : MonoBehaviour
{

    public GameObject dharahara;
    int spawnObjectEveryNTiles = 13;
    int tilesCount = 0;

    GroundSpawner groundSpawner;
    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        tilesCount = groundSpawner.GetTilesSpawnedCount();
        SpawnDharahara();
    }

   
    public void SpawnDharahara()
    {
        int obstacleSpawnx = 2;
        Transform spawnRight = transform.GetChild(obstacleSpawnx).transform;
        if (tilesCount == 0)
        {
            Instantiate(dharahara, spawnRight.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
            tilesCount++;
            return;
        }
        if ((tilesCount - 1) % spawnObjectEveryNTiles == 0)
        {
            Instantiate(dharahara, spawnRight.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
        }
        tilesCount++;

    }
}
