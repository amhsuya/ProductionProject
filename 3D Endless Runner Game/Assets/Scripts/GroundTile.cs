using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
 
    public GameObject obstaclePrefab1;
    public GameObject obstaclePrefab2;

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
        //  int obstacleSpawnIndex = Random.Range(2, 5);
        // Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        // Randomly select one of the two obstacle prefabs


        //  Instantiate(obstaclePrefab1, spawnPoint.position, Quaternion.identity, transform);

        int obstacleSpawnIndex1 = Random.Range(2, 5);
        int obstacleSpawnIndex2 = Random.Range(2, 5);

        while (obstacleSpawnIndex2 == obstacleSpawnIndex1)
        {
            obstacleSpawnIndex2 = Random.Range(2, 5);
        }

        Transform spawnPoint1 = transform.GetChild(obstacleSpawnIndex1).transform;
        Transform spawnPoint2 = transform.GetChild(obstacleSpawnIndex2).transform;

        bool spawnFirstObstacle = Random.Range(0, 2) == 0; // Randomly choose which obstacle to spawn first

        // Spawn the first obstacle
        if (spawnFirstObstacle)
        {
            GameObject obstaclePrefab = Random.Range(0, 2) == 0 ? obstaclePrefab1 : obstaclePrefab2;
            Vector3 spawnPosition = spawnPoint1.position;
            spawnPosition.y = obstaclePrefab.transform.position.y; // Set the correct y-position
            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity, spawnPoint1);
        }

        // Spawn the second obstacle
        if (!spawnFirstObstacle)
        {
            GameObject obstaclePrefab = Random.Range(0, 2) == 0 ? obstaclePrefab1 : obstaclePrefab2;
            Vector3 spawnPosition = spawnPoint2.position;
            spawnPosition.y = obstaclePrefab.transform.position.y; // Set the correct y-position
            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity, spawnPoint2);
        }

    }


   public GameObject coinPrefab;
    void SpawnCoins()
    {
        int coinsToSpawn = 10;
        float[] laneXPositions = { -3.3f, 0f, 3.3f }; // Pre-defined x-axis positions for the lanes
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
