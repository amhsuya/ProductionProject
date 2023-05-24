using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float horizontalMultiplier = 3.3f;

    public Rigidbody rgbody;

    bool alive = true;


    public float laneWidth = 2f; // distance between lanes
    public int currentLane = 0; // start in middle lane

    private Vector2 swipeStartPosition;

    private bool isSwiping = false;
    private bool isJumping = false;

    public float jumpForce = 2f;

    public float increaseSpeed = 0.5f;

    public AudioClip jumpSound;
    public AudioClip swipeSound;

    public bool hasMagnet = false;
    public float magnetDuration = 10f;
    private float magnetTimer = 0f;
    public float magnetRadius = 25f;

    private bool rotatingPlayer = false;
    private bool movingCamera = false;
    private Quaternion targetRotation;
    private Vector3 originalCameraPosition;
    private Vector3 targetCameraPosition;
    private float rotationSpeed = 5f;
    private float cameraMoveDistance = 2f;
    private float cameraMoveDuration = 1f;


  

    /// <summary>
    /// Components are initialized 
    /// Called by engine automatically at the beginning
    /// </summary>
    void Start()
    {

        currentLane = 0; // start in a random lane
        rgbody = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -20f, 0);
        rgbody.mass = 1f; //mass of rigid body
        rgbody.drag = 0.5f; //dragged by gravity
    }
    /// <summary>
    /// Called at fixed interval
    /// calculates and updates value of player to move forward, horizontal and up 
    /// </summary>
    private void FixedUpdate()
    {
        if (!alive) return;

        //moves player forward
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;


        float targetX = currentLane * laneWidth;
        float deltaX = targetX - transform.position.x;
        Vector3 horizontalMove = Vector3.right * deltaX / laneWidth * speed * horizontalMultiplier * Time.fixedDeltaTime;

        if (isJumping == true)
        {
           
            rgbody.AddForce(Vector3.up * jumpForce);
            isJumping = false;
        }
        rgbody.MovePosition(rgbody.position + forwardMove + horizontalMove);
    }

    /// <summary>
    /// Called once per frame
    /// checks for user input and updates
    /// </summary>
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
                    AudioSource.PlayClipAtPoint(swipeSound, transform.position, 0.5f);
                }
                else if (deltaY > 50 && !isJumping && rgbody.velocity.y == 0 && Mathf.Abs(deltaX) < 50)
                {
                    //rgbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    isJumping = true;
                    isSwiping = false;
                    AudioSource.PlayClipAtPoint(jumpSound, transform.position, 0.3f);

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
        speed += increaseSpeed * Time.deltaTime;


        if (hasMagnet)
        {
            magnetTimer -= Time.deltaTime;
            if (magnetTimer <= 0)
            {
                DeactivateMagnet();
            }
            else
            {
                Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, magnetRadius);
                foreach (Collider collider in nearbyColliders)
                {
                    if (collider.CompareTag("Coin"))
                    {

                        Coin coin = collider.GetComponent<Coin>();
                        if (coin != null)
                        {
                            coin.isAttracted = true;
                        }
                    }
                }
            }
        }

        if (rotatingPlayer)
        {
            RotatePlayer();
        }

    }
    /// <summary>
    /// Character dies and resets gravity and restart game after a second
    /// </summary>
    /// 
    public Transform mainCamera;
    public void Die()
    {
        alive = false;

        targetRotation = Quaternion.Euler(-90f, transform.rotation.eulerAngles.y, 0f);
        rotatingPlayer = true;
        movingCamera = true;

        rgbody.constraints = RigidbodyConstraints.None;
        Invoke("Restart", 1);
        Physics.gravity = new Vector3(0, -9.81f, 0);

        StartCoroutine(MoveCameraBack(cameraMoveDistance, cameraMoveDuration));

    }
    private void RotatePlayer()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (Quaternion.Angle(transform.rotation, targetRotation) <= 0.01f)
        {
            rotatingPlayer = false;
        }
    }

    IEnumerator MoveCameraBack(float distance, float duration)
    {
        originalCameraPosition = mainCamera.position;
        targetCameraPosition = originalCameraPosition - Vector3.up * distance;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            mainCamera.position = Vector3.Lerp(originalCameraPosition, targetCameraPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.position = targetCameraPosition;
    }

    /// <summary>
    /// Loads a new scene where
    /// Game objects and variable are reset to their initial state after player has lost
    /// </summary>
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public float GetSpeed()
    {
        return speed;
    }
    public void ActivateMagnet()
    {
        hasMagnet = true;
        magnetTimer = magnetDuration;
        // Add any additional effects when the magnet is activated (e.g., visual effects)
    }

    public void DeactivateMagnet()
    {
        hasMagnet = false;
        magnetTimer = 0f;
        // Add any additional effects when the magnet is deactivated (e.g., visual effects)
    }

}
