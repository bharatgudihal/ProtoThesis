﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadToSidescroll : MonoBehaviour {

    [SerializeField]
    private BoxCollider sidescrollArea;
    [SerializeField]
    private BoxCollider overheadArea;
    [SerializeField]
    private Transform overheadCameraPosition;
    [SerializeField]
    private Transform sideScrollCameraPosition;
    [SerializeField]
    private Transform sideScrollLockToAxis;

    public float timeToTween;

    private JoystickMovement joystickMovement;
    private SidescrollCamera sidescrollCamera;
    private CameraLock cameraLock;

    private float tweenTimer;

    private void Awake()
    {
        joystickMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<JoystickMovement>();
        sidescrollCamera = Camera.main.gameObject.GetComponent<SidescrollCamera>();
        cameraLock = Camera.main.gameObject.GetComponent<CameraLock>();
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        tweenTimer += Time.deltaTime;

        if (LeanTween.isTweening(Camera.main.gameObject) && tweenTimer < timeToTween)
        {
            LeanTween.cancel(Camera.main.gameObject);
            LeanTween.move(Camera.main.gameObject, joystickMovement.transform.position - cameraLock.distance, timeToTween - tweenTimer).setOnComplete(LockCamera);
            LeanTween.rotate(Camera.main.gameObject, cameraLock.angle, timeToTween - tweenTimer);
        }
    }

    public void SidescrollTriggerEnter()
    {
        if (CameraLock.cameraMode != CameraMode.SIDESCROLL)
        {
            joystickMovement.transform.position = new Vector3(sideScrollLockToAxis.position.x, joystickMovement.transform.position.y, sideScrollLockToAxis.position.z);
            LeanTween.move(Camera.main.gameObject, sideScrollCameraPosition.position, timeToTween).setOnComplete(LockCamera);
            LeanTween.rotate(Camera.main.gameObject, sideScrollCameraPosition.eulerAngles, timeToTween);
            tweenTimer = 0f;
            cameraLock.UnlockCamera();
            joystickMovement.isSidescrolling = true;
            cameraLock.distance = sidescrollArea.transform.position - sideScrollCameraPosition.position;
            cameraLock.angle = sideScrollCameraPosition.eulerAngles;
            CameraLock.cameraMode = CameraMode.SIDESCROLL;
        }
    }

    public void SidescrollTriggerStay()
    {

    }

    public void SidescrollTriggerExit()
    {

    }

    public void OverheadTriggerEnter()
    {
        if (CameraLock.cameraMode != CameraMode.OVERHEAD)
        {
            LeanTween.move(Camera.main.gameObject, overheadCameraPosition.position, timeToTween).setOnComplete(LockCamera);
            LeanTween.rotate(Camera.main.gameObject, overheadCameraPosition.eulerAngles, timeToTween);
            tweenTimer = 0f;
            cameraLock.UnlockCamera();
            joystickMovement.isSidescrolling = false;
            cameraLock.distance = joystickMovement.transform.position - overheadCameraPosition.position;
            cameraLock.angle = overheadCameraPosition.eulerAngles;
            CameraLock.cameraMode = CameraMode.OVERHEAD;
        }
    }

    public void OverheadTriggerStay()
    {

    }

    public void OverheadTriggerExit()
    {

    }

    public void LockCamera()
    {
        cameraLock.LockCamera();
    }

    public void UnlockCamera()
    {
        cameraLock.UnlockCamera();
    }

}
