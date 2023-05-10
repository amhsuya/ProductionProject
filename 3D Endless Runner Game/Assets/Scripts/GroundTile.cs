using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
 
    public GameObject obstaclePrefab;
   

    public GameObject leftTile;
   public GameObject rightTile;


    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
 
    
        SpawnObstacle();
        SpawnCoins();

    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile();

        Destroy(leftTile, 2);
        Destroy(rightTile, 2);
        Destroy(gameObject, 2);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObstacle()
    {
        // Choose random point to spawn the obstacle
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        //Spawn the obstacle
        //quaternion no rotation
        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
    }


   public GameObject coinPrefab;
    void SpawnCoins()
    {
        int coinsToSpawn = 10;
        float[] laneXPositions = { -2.5f, 0f, 2.5f }; // Pre-defined x-axis positions for the lanes
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);
            Vector3 coinSpawnPoint = GetRandomPointInCollider(GetComponent<Collider>(), laneXPositions);
            temp.transform.position = coinSpawnPoint;
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider,float[] laneXPositions)
    {
        float randomXPosition = laneXPositions[Random.Range(0, laneXPositions.Length)]; // Choose random x-axis position from the pre-defined lane positions
        Vector3 point = new Vector3(
            randomXPosition,
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        );
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider, laneXPositions);
        }
        point.y = 0.5f; // Set the coin to spawn slightly above the ground
        return point;
    }

 


}
