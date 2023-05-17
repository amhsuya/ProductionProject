using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSideSpawner1 : MonoBehaviour
{

    public GameObject dharahara;
    public GameObject nyatapola;

    int spawnObjectEveryNTiles = 15;
    int spawnNyatapolaEveryNTiles = 17;

    int tilesCount = 0;

    private PlayerMovement playerMovement;
    int dharaharaSpawnDistance = 15;
    int nyatapolaSpawnDistance = 17;

    GroundSpawner groundSpawner;
    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();

       
        SpawnDharahara();
        SpawnNyatapola();
    }

   
    public void SpawnDharahara()
    {
        int obstacleSpawnx = 2;
        Transform spawnRight = transform.GetChild(obstacleSpawnx).transform;
        tilesCount = groundSpawner.GetTilesSpawnedCount();
        float adjustedDharaharaSpawnDistance = dharaharaSpawnDistance * playerMovement.GetSpeed();

        if (tilesCount >= adjustedDharaharaSpawnDistance && (tilesCount - adjustedDharaharaSpawnDistance) % spawnObjectEveryNTiles == 0)
        {
            int spawnTileIndex = (int)(tilesCount - adjustedDharaharaSpawnDistance) / spawnObjectEveryNTiles;
            Vector3 dharaharaPosition = spawnRight.position + new Vector3(0, 1, spawnTileIndex * 1);
            Instantiate(dharahara, dharaharaPosition + new Vector3(0, 0, 15 * 1), Quaternion.identity, transform);
        }

    }
    public void SpawnNyatapola()
    {

        int obstacleSpawnx = 3;
        Transform spawnRight = transform.GetChild(obstacleSpawnx).transform;
        tilesCount = groundSpawner.GetTilesSpawnedCount();
        float adjustedNyatapolaSpawnDistance = nyatapolaSpawnDistance * playerMovement.GetSpeed();

        if (tilesCount >= adjustedNyatapolaSpawnDistance && (tilesCount - adjustedNyatapolaSpawnDistance) % spawnNyatapolaEveryNTiles == 0)
        {
            int spawnTileIndex = (int)(tilesCount - adjustedNyatapolaSpawnDistance) / spawnNyatapolaEveryNTiles;
            Vector3 nyatapolaPosition = spawnRight.position + new Vector3(0, 1, spawnTileIndex * 1);
            Instantiate(nyatapola, nyatapolaPosition + new Vector3(0, 15, 17 * 1), Quaternion.identity, transform);
        }
    }
}
