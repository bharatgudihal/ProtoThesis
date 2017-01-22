using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollArea : MonoBehaviour {

    public Transform newCameraPosition;
    public float timeTaken;
    public Vector3 positionWhenPlayerHitArea;
    public Vector3 lockToAxis;
    public bool isSidescrolling;
    public Vector3 distance;

    private GameObject mainCamera;
    private Transform player;
    private float tweenTimer;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main.gameObject;
        lockToAxis = transform.GetChild(0).gameObject.transform.position;
        distance = lockToAxis - newCameraPosition.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !player.GetComponent<JoystickMovement>().isSidescrolling)
        {
            LeanTween.cancel(Camera.main.gameObject);
            player.transform.position = new Vector3(lockToAxis.x, player.position.y, lockToAxis.z);
            LeanTween.move(Camera.main.gameObject, newCameraPosition.position, timeTaken).setOnComplete(SetCameraActive);
            LeanTween.rotate(Camera.main.gameObject, newCameraPosition.eulerAngles, timeTaken).setOnComplete(SetCameraActive);
            tweenTimer = 0f;
            mainCamera.GetComponent<OverheadCamera>().enabled = false;
            mainCamera.GetComponent<SidescrollCamera>().SetCameraPosition(newCameraPosition.position);
            other.GetComponent<JoystickMovement>().isSidescrolling = true;
            positionWhenPlayerHitArea = other.transform.position;
        }
    }

    private void Update()
    {
        tweenTimer += Time.deltaTime;
        if (LeanTween.isTweening(mainCamera) && positionWhenPlayerHitArea != player.position)
        {
            LeanTween.cancel(mainCamera);
            LeanTween.move(Camera.main.gameObject, player.position - distance, timeTaken - tweenTimer).setOnComplete(SetCameraActive);
            LeanTween.rotate(Camera.main.gameObject, newCameraPosition.eulerAngles, timeTaken - tweenTimer).setOnComplete(SetCameraActive);
        }
    }

    private void SetCameraActive()
    {
        mainCamera.GetComponent<SidescrollCamera>().enabled = true;
    }
}
