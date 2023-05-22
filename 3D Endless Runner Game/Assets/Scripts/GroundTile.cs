using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
 
    public GameObject obstaclePrefab1;
    public GameObject obstaclePrefab2;

    public GameObject powerUpPrefab;

    public GameObject leftTile;
    public GameObject rightTile;

    private Transform tileTransform;

    public GameObject coinPrefab;
    private List<GameObject> coinPool = new List<GameObject>();
    private int coinsToSpawn = 5;

    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();

        tileTransform = transform;

        SpawnObstacle();

        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform);
            coin.SetActive(false);
            coinPool.Add(coin);
        }

        SpawnCoins();
        SpawnPowerUp();

    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile();

        Destroy(leftTile, 2);
        Destroy(rightTile, 2);
        Destroy(gameObject, 2);
        
    }
    private void SpawnPowerUp()
    {
        int powerUpSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(powerUpSpawnIndex).transform;
        bool spawnPowerUp = Random.Range(0, 60) == 0; 


        if (spawnPowerUp)
        {
            bool spawnObstacleOnPowerUpSpot = spawnPoint.GetComponentInChildren<Obstacle>() != null;

            if (!spawnObstacleOnPowerUpSpot)
            {
                Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
                
            }
        }
    }



    
    void SpawnObstacle()
    {
        int obstacleSpawnCount = 0;
        int maxObstaclesToSpawn = 1;

        while (obstacleSpawnCount < maxObstaclesToSpawn)
        {
            int obstacleSpawnIndex = Random.Range(2, 5);
            Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

            GameObject obstaclePrefab = Random.Range(0, 2) == 0 ? obstaclePrefab1 : obstaclePrefab2;
            Vector3 spawnPosition = spawnPoint.position;
            spawnPosition.y = obstaclePrefab.transform.position.y; // Set the correct y-position
            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity, spawnPoint);

            obstacleSpawnCount++;
        }
    }
    void InstantiateObstacle(GameObject prefab, Transform spawnPoint)
    {
        Vector3 spawnPosition = spawnPoint.position;
        spawnPosition.y = prefab.transform.position.y; // Set the correct y-position
        Instantiate(prefab, spawnPosition, Quaternion.identity, spawnPoint);
    }

   
    void SpawnCoins()
    {
        float[] laneXPositions = { -3.3f, 0f, 3.3f };

        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject coin = GetPooledCoin();
            coin.transform.position = GetRandomPointInCollider(GetComponent<Collider>(), laneXPositions);
            coin.SetActive(true);
        }
    }
    GameObject GetPooledCoin()
    {
        foreach (GameObject coin in coinPool)
        {
            if (!coin.activeInHierarchy)
            {
                return coin;
            }
        }

        return null; // Handle the case when all coins are active
    }
    Vector3 GetRandomPointInCollider(Collider collider,float[] laneXPositions)
    {
        Vector3 point;
        float randomXPosition;

        do
        {
            randomXPosition = laneXPositions[Random.Range(0, laneXPositions.Length)];
            point = new Vector3(
                randomXPosition,
                Random.Range(collider.bounds.min.y, collider.bounds.max.y),
                Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
            point.y = 0.5f; // Set the coin to spawn slightly above the ground
        } while (point != collider.ClosestPoint(point));

        return point;
    }

 


}
