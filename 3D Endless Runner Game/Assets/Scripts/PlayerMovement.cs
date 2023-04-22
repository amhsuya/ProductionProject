
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody rgbody;
    float horizontalInput;
    public float horizontalMultiplier = 2;
    bool alive = true;

    private void FixedUpdate()
    {
        if (!alive)  return;
        
        //50 times 
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        //moves horizontally twice as faster
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rgbody.MovePosition(rgbody.position + forwardMove + horizontalMove);
    }



    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (transform.position.y < -5)
        {
            Die();

        }
    }

    public void Die()
    {
        alive = false;

        Invoke("Restart", 1);

    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
