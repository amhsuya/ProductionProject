using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //speed of coin rotating
    public float turnSpeed = 90f;
    public AudioClip coinSound;


    public bool isAttracted = false;
    private Transform playerTransform;
    private float attractionForce = 10f;
    private float attractionRange = 5f;
    private float attractionSpeed = 15f;


    /// <summary>
    /// If player collides with coin then score is incremented
    /// else it is returned
    /// if collided object does not have obstace component then func is returned
    /// </summary>
    /// <param name="coin"></param>
    private void OnTriggerEnter(Collider coin)
    {
        if(coin.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }
        else if(coin.gameObject.name != "Player")
        {
           
            return;
        }
        else if (coin.CompareTag("Power"))
        {
           
          isAttracted = true;
            Destroy(coin.gameObject);
        }
        AudioSource.PlayClipAtPoint(coinSound, transform.position,.5f);
        GameManager.instance.IncrementScore();

        Destroy(gameObject);


    }
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
  
    // Update is called once per frame
    /// <summary>
    /// Rotates coin
    /// </summary>
    void Update()
    {
        if (isAttracted)
        {
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer <= attractionRange)
            {
                float step = attractionSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position + playerTransform.forward * 3, step );
            }
        }
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
        
    }
}
