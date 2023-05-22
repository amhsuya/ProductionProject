using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public float distanceThreshold = 5f; // The distance threshold for destroying the gate
    private GameObject player; // Reference to the player object
    private bool playerInRange = false; // Flag to track if the player is in range of the gate

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Assuming the player has a tag "Player"
    }

    private void Update()
    {
        if (!playerInRange)
        {
            // Calculate the distance between the gate and the player
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Check if the player has moved within the distance threshold
            if (distance <= distanceThreshold)
            {
                playerInRange = true;
            }
        }
        else
        {
            // Calculate the distance between the gate and the player
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Check if the player has moved away beyond the distance threshold
            if (distance > distanceThreshold)
            {
                Destroy(gameObject); // Destroy the gate game object
            }
        }
    }
}
