using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSideSpawner1 : MonoBehaviour
{

    public GameObject dharaharaPrefab;
    public GameObject nyatapolaPrefab;

    int spawnObjectEveryNTiles = 15;
   

    int tilesCount = 0;

   
    int dharaharaSpawnDistance = 15;
   
    GroundSpawner groundSpawner;

    private GameObject currentObstacle;


    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();

        ResetTilesCount();

        tilesCount = groundSpawner.GetTilesSpawnedCount();
        SpawnRightSideScene();        

    }
  

    public void SpawnRightSideScene()
    {
        if (tilesCount % spawnObjectEveryNTiles == 0)
        {
            if (currentObstacle != null)
            {
                Destroy(currentObstacle);
             
            }
            int obstacleSpawnIndex = (tilesCount / spawnObjectEveryNTiles) % 2; // Alternates between 0 and 1
            Transform spawnRight = transform.GetChild(obstacleSpawnIndex + 2).transform;
            if (obstacleSpawnIndex == 0)
            {
                currentObstacle = Instantiate(dharaharaPrefab, spawnRight.position + new Vector3(0,1,0), Quaternion.identity, transform);
            }
            else
            {
                currentObstacle = Instantiate(nyatapolaPrefab, spawnRight.position + new Vector3(1, 10, 1), Quaternion.identity, transform);
            }

            currentObstacle.transform.parent = spawnRight;
           
         }

        tilesCount++;

    }

    public void ResetTilesCount()
    {
        tilesCount = 0;
    }
    public void RestartGame()
    {
        if (currentObstacle != null)
        {
            Destroy(currentObstacle);
        }

        ResetTilesCount();
        SpawnRightSideScene();
    }
}
