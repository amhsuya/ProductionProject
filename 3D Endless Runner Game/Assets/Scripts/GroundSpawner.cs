using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    Vector3 nextSpawnPoint;

    public GameObject backDrop;

   public GameObject leftSpawnPoint;
    Vector3 nextLeftSpawnPoint;

    public GameObject rightSpawnPoint;
    Vector3 nextRightSpawnPoint;

    public int tilesSpawnedCount = 0;


    void Start()
    {
       
        for(int i = 0; i < 19; i++)
        {
            SpawnTile();
           
        }
    }
   void Update()
    {
        float playerZ = Camera.main.transform.position.z;
        playerZ += 900;
        Vector3 backgroundMovement = new Vector3(0, 120, playerZ);
        backDrop.transform.position = backgroundMovement;
    }
  
    public void SpawnTile()
    {
        GameObject newTile = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = newTile.transform.GetChild(1).transform.position;

        GameObject newLeftTile = Instantiate(leftSpawnPoint, nextLeftSpawnPoint, Quaternion.identity);
        nextLeftSpawnPoint = newLeftTile.transform.GetChild(1).transform.position;

          GameObject newRightTile = Instantiate(rightSpawnPoint, nextRightSpawnPoint, Quaternion.identity);
        nextRightSpawnPoint = newRightTile.transform.GetChild(1).transform.position;

        GroundTile ground = newTile.GetComponent<GroundTile>();
        ground.leftTile = newLeftTile;
        ground.rightTile = newRightTile;

        tilesSpawnedCount++;
        
    }

    public int GetTilesSpawnedCount()
    {
        return tilesSpawnedCount;
    }
  
    
}
