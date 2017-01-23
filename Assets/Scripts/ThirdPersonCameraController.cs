using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour {

    [SerializeField]
    float followDistance;

    [SerializeField]
    float maxX;

    [SerializeField]
    float maxY;

    [SerializeField]
    float smoothTime;

    [SerializeField]
    float sensitivity;

    [SerializeField]
    Transform dummyCamera;

    Transform lookAt;
    Vector3 followDistanceVector;

    Vector3 velocity;

    float moveX;
    float moveY;
    public bool isUpdating;
    public Vector3 positionToMoveTo;
    public Vector3 angleToRotateTo;

    private void Awake()
    {
        lookAt = GameObject.Find("LookAt").GetComponent<Transform>();
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void FixedUpdate () {        
        followDistanceVector = Vector3.forward * -followDistance;
        moveX += Input.GetAxis("Camera X") * sensitivity;
        moveY += Input.GetAxis("Camera Y") * sensitivity;
        moveX = Mathf.Clamp(moveX, -maxY, maxY);
        //moveY = Mathf.Clamp(moveY, -maxX, maxX);
        Vector3 newPosition = lookAt.position + Quaternion.Euler(moveX, moveY, 0) * followDistanceVector;
        positionToMoveTo = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        dummyCamera.position = positionToMoveTo;
        dummyCamera.LookAt(lookAt);
        angleToRotateTo = dummyCamera.transform.rotation.eulerAngles;

        if (isUpdating)
        {

            transform.position = positionToMoveTo;
            transform.LookAt(lookAt);
        }
    }
}
