using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSideSpawner : MonoBehaviour
{
    public GameObject rightSidePlane;
    Vector3 nextSpawnPnt;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnRightTile();
        }
    }
    public void SpawnRightTile()
    {
        GameObject temp = Instantiate(rightSidePlane, nextSpawnPnt, Quaternion.identity);
        nextSpawnPnt = temp.transform.GetChild(1).transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
