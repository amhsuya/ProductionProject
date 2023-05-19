using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSideSpawner : MonoBehaviour
{
    public GameObject stupaPrefab;
    public GameObject rathPrefab;
    public int initialPoolSize = 5;

    private List<GameObject> stupaPool;
    private List<GameObject> rathPool;

    private int spawnObjectEveryNTiles = 15;
    private int tilesCount = 0;

    private GroundSpawner groundSpawner;
    private Transform[] obstacleSpawnPoints;

    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        tilesCount = groundSpawner.GetTilesSpawnedCount();
        obstacleSpawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            obstacleSpawnPoints[i] = transform.GetChild(i);
        }

        InitializeObjectPool();
        SpawnStupa();
        SpawnRath();
    }

    private void InitializeObjectPool()
    {
        stupaPool = new List<GameObject>();
        rathPool = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject stupa = Instantiate(stupaPrefab, Vector3.zero, Quaternion.identity, transform);
            GameObject rath = Instantiate(rathPrefab, Vector3.zero, Quaternion.identity, transform);

            stupa.SetActive(false);
            rath.SetActive(false);

            stupaPool.Add(stupa);
            rathPool.Add(rath);
        }
    }

    public void SpawnStupa()
    {
        int obstacleSpawnIndex = 2;
        Transform spawnPoint = obstacleSpawnPoints[obstacleSpawnIndex];

        if (tilesCount % spawnObjectEveryNTiles == 0)
        {
            GameObject stupa = GetPooledObject(stupaPool);

            if (stupa != null)
            {
                stupa.transform.position = spawnPoint.position + new Vector3(0, 1, 0);
                stupa.SetActive(true);
            }
        }

        tilesCount++;
    }

    public void SpawnRath()
    {
        int obstacleSpawnIndex = 4;
        Transform spawnPoint = obstacleSpawnPoints[obstacleSpawnIndex];

        if (tilesCount % spawnObjectEveryNTiles == 0)
        {
            GameObject rath = GetPooledObject(rathPool);

            if (rath != null)
            {
                rath.transform.position = spawnPoint.position + new Vector3(-6.6f, 1, 0);
                rath.transform.rotation = Quaternion.Euler(0, 0, -90);
                rath.SetActive(true);
            }
        }

        tilesCount++;
    }

    private GameObject GetPooledObject(List<GameObject> pool)
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // If no inactive objects are found, create a new one
        GameObject newObj = Instantiate(stupaPrefab, Vector3.zero, Quaternion.identity, transform);
        pool.Add(newObj);

        return newObj;
    }

    public void RestartGame()
    {
        foreach (GameObject stupa in stupaPool)
        {
            stupa.SetActive(false);
        }

        foreach (GameObject rath in rathPool)
        {
            rath.SetActive(false);
        }

        tilesCount = 0;

        SpawnStupa();
        SpawnRath();
    }
}
