
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody rgbody;

    public float horizontalMultiplier = 3f;
    bool alive = true;


    public float laneWidth = 2; // distance between lanes
    public int currentLane = 0; // start in middle lane
    private Vector2 swipeStartPosition;
    private bool isSwiping = false;

    private bool isJumping = false;
    public float jumpForce = 2f;

    void Start()
    {
        currentLane = 0; // start in a random lane
        rgbody = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -20f, 0);
        rgbody.mass = 1f;
        rgbody.drag = 0.5f;
    }
    private void FixedUpdate()
    {
        if (!alive)  return;
        
        //50 times 
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        

        float targetX = currentLane * laneWidth;
        float deltaX = targetX - transform.position.x;
        Vector3 horizontalMove = Vector3.right * deltaX / laneWidth *speed * horizontalMultiplier * Time.fixedDeltaTime;

        if(isJumping == true)
        {
            rgbody.AddForce(Vector3.up * jumpForce);
            isJumping = false;
        }
        rgbody.MovePosition(rgbody.position + forwardMove + horizontalMove);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1) // only handle single touch
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swipeStartPosition = touch.position;
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                float deltaX = touch.position.x - swipeStartPosition.x;
                float deltaY = touch.position.y - swipeStartPosition.y;
                if (Mathf.Abs(deltaX) > 50) // threshold to detect swipe
                {
                    // determine the target lane based on the current lane and swipe direction
                    int targetLane = currentLane;
                    if (currentLane == 0) // middle lane
                    {
                        if (deltaX > 0) // swipe to right
                        {
                            targetLane = 1; // move to right lane
                        }
                        else // swipe to left
                        {
                            targetLane = -1; // move to left lane
                        }
                    }
                    else if (currentLane == -1) // left lane
                    {
                        if (deltaX > 0) // swipe to right
                        {
                            targetLane = 0; // move to middle lane
                        }
                    }
                    else if (currentLane == 1) // right lane
                    {
                        if (deltaX < 0) // swipe to left
                        {
                            targetLane = 0; // move to middle lane
                        }
                    }

                    // set the current lane to the target lane
                    currentLane = targetLane;

                    isSwiping = false;
                }
                else if(deltaY > 50 && !isJumping && rgbody.velocity.y ==0 && Mathf.Abs(deltaX) < 50)
                {
                    //rgbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    isJumping = true;
                    isSwiping = false;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isSwiping = false;
            }
        }

        if (transform.position.y < -5)
        {
            Die();

        }
    }

    public void Die()
    {
        alive = false;
        rgbody.constraints = RigidbodyConstraints.None;
        Invoke("Restart", 1);
        Physics.gravity = new Vector3(0, -9.81f, 0);

    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
