using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollToThirdPerson : MonoBehaviour {

    [SerializeField]
    private BoxCollider thirdPersonArea;
    [SerializeField]
    private BoxCollider sidescrollArea;
    [SerializeField]
    private Transform sideScrollCameraPosition;
    [SerializeField]
    private Transform sideScrollLockToAxis;

    public float timeToTween;

    private JoystickMovement joystickMovement;
    private CameraLock cameraLock;
    private ThirdPersonCameraController thirdPersonCamera;

    private float tweenTimer;

    void Awake()
    {
        joystickMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<JoystickMovement>();
        cameraLock = Camera.main.gameObject.GetComponent<CameraLock>();
        thirdPersonCamera = Camera.main.gameObject.GetComponent<ThirdPersonCameraController>();
    }


    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        tweenTimer += Time.deltaTime;

        if (LeanTween.isTweening(Camera.main.gameObject) && tweenTimer < timeToTween)
        {
            if (CameraLock.cameraMode == CameraMode.SIDESCROLL)
            {
                LeanTween.cancel(Camera.main.gameObject);
                LeanTween.move(Camera.main.gameObject, joystickMovement.transform.position - cameraLock.distance, timeToTween - tweenTimer).setOnComplete(LockCamera);
                LeanTween.rotate(Camera.main.gameObject, cameraLock.angle, timeToTween - tweenTimer);
            }
            else if (CameraLock.cameraMode == CameraMode.THIRDPERSON)
            {
                LeanTween.cancel(Camera.main.gameObject);
                LeanTween.move(Camera.main.gameObject, thirdPersonCamera.positionToMoveTo, timeToTween - tweenTimer).setOnComplete(StartUpdatingThirdPersonCamera);
                LeanTween.rotate(Camera.main.gameObject, thirdPersonCamera.angleToRotateTo, timeToTween - tweenTimer);
            }
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
            StopUpdatingThirdPersonCamera();
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

    public void ThirdPersonTriggerEnter()
    {
        if (CameraLock.cameraMode != CameraMode.THIRDPERSON)
        {
            CameraLock.cameraMode = CameraMode.THIRDPERSON;
            LeanTween.move(Camera.main.gameObject, thirdPersonCamera.positionToMoveTo, timeToTween).setOnComplete(StartUpdatingThirdPersonCamera);
            LeanTween.rotate(Camera.main.gameObject, thirdPersonCamera.angleToRotateTo, timeToTween);
            cameraLock.UnlockCamera();
            joystickMovement.isSidescrolling = false;
            tweenTimer = 0f;

        }
    }

    public void ThirdPersonTriggerStay()
    {

    }

    public void ThirdPersonTriggerExit()
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

    public void StartUpdatingThirdPersonCamera()
    {
        thirdPersonCamera.isUpdating = true;
    }

    public void StopUpdatingThirdPersonCamera()
    {
        thirdPersonCamera.isUpdating = false;
    }
}
