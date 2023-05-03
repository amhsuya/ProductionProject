using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSideSpawner : MonoBehaviour
{
   public GameObject leftSidePlane;
    Vector3 nextSpawnPnt;

    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            SpawnLeftTile();
        }
      
    }

    public void SpawnLeftTile()
    {
        GameObject temp = Instantiate(leftSidePlane, nextSpawnPnt, Quaternion.identity);
        nextSpawnPnt = temp.transform.GetChild(1).transform.position;
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }
}
