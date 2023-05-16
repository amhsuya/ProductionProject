using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;
    public bool isDeadlyJump = false;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collison)
    {
        if(collison.gameObject.name == "Player")
        {
            playerMovement.Die();
        }
      
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isDeadlyJump == true && other.gameObject.name == "Player")
        {
            playerMovement.Die();
        }
    }
}
