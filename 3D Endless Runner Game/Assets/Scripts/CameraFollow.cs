
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    Vector3 offset;
    // Start is called before the first frame update
    /// <summary>
    /// set distance between camera and player
    /// </summary>
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    /// <summary>
    /// updates camera position to follow player
    /// </summary>
    void Update()
    {
        // x and y coordinate of curent camera position and z(calculated dist betn player and camera)
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z);

        Vector3 desiredPosition = player.transform.position + offset;

        if (player.GetComponent<Rigidbody>().velocity.y > 0)
        {
            desiredPosition.y += 0.5f; // add an offset to the Y position
        }

        transform.position = desiredPosition;

        transform.position = desiredPosition;
        // lerp interpolates current pos of camera and target position at certain speed 10* Time.deltaTime
        transform.position = Vector3.Lerp(transform.position, targetPosition, 10 * Time.deltaTime);
    }
}
