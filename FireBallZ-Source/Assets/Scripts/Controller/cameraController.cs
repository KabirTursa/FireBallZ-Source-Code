using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class cameraController : MonoBehaviour
{
    public float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    private float lookAhead;
    public float aheadDistance;
    public float cameraSpeed;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

 
}
*/

public class cameraController : MonoBehaviour
{
    public float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    private float lookAhead;
    public float aheadDistance;
    public float cameraSpeed;
    public Transform player;

    // Zoom related variables
    public float zoomSpeed = 1.0f; // Adjust this value to control the speed of zooming
    public float minSize = 5f; // The minimum orthographic size (zoomed out)
    public float maxSize = 10f; // The maximum orthographic size (zoomed in)

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        // Move the camera to follow the player horizontally
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        // Zooming controls
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float newSize = cam.orthographicSize - scroll * zoomSpeed;

        // Clamp the size to stay within the minSize and maxSize bounds
        newSize = Mathf.Clamp(newSize, minSize, maxSize);

        // Apply the new size to the camera
        cam.orthographicSize = newSize;
    }
}
