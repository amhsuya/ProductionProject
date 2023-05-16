using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //speed of coin rotating
    public float turnSpeed = 90f;
    public AudioClip coinSound;

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
        if(coin.gameObject.name != "Player")
        {
            return;
        }
        AudioSource.PlayClipAtPoint(coinSound, transform.position, 1);
        GameManager.instance.IncrementScore();

        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /// <summary>
    /// Rotates coin
    /// </summary>
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}
