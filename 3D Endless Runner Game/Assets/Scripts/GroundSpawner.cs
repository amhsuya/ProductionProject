
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject GroundTile;
    Vector3 nextSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 15; i++)
        {
            SpawnTile();
        }
    }
    public void SpawnTile()
    {
        GameObject temp = Instantiate(GroundTile, nextSpawnPoint, Quaternion.identity);
        // obj to spawn, where to spawn, rotation

        nextSpawnPoint =temp.transform.GetChild(1).transform.position;
        // grand tile newspawnpoint in 1 index
        //get transform component
    }

    
}
