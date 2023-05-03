using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    Vector3 nextSpawnPoint;

    public GameObject backDrop;

 

    void Start()
    {
       
        for(int i = 0; i < 15; i++)
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
    }

    
}
