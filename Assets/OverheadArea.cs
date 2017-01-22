using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadArea : MonoBehaviour {

    public OverheadCamera overheadCamera;
    public JoystickMovement joystickMovement;
    public float timeTaken;
    public Transform newCameraPosition;


    private float tweenTimer;
    public SidescrollCamera sidescrollCamera;

    private void Awake()
    {
        overheadCamera = Camera.main.gameObject.GetComponent<OverheadCamera>();
        sidescrollCamera = Camera.main.gameObject.GetComponent<SidescrollCamera>();
        joystickMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<JoystickMovement>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        tweenTimer += Time.deltaTime;
        if (LeanTween.isTweening(Camera.main.gameObject) && joystickMovement.isSidescrolling == false)
        {
            LeanTween.cancel(Camera.main.gameObject);
            Debug.Log(newCameraPosition.position);
            Debug.Log(newCameraPosition.transform.rotation.eulerAngles);
            LeanTween.move(Camera.main.gameObject, newCameraPosition.position, timeTaken - tweenTimer).setOnComplete(SetCameraActive);
            LeanTween.rotate(Camera.main.gameObject, newCameraPosition.transform.rotation.eulerAngles, timeTaken - tweenTimer).setOnComplete(SetCameraActive);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && joystickMovement.isSidescrolling)
        {
            LeanTween.cancel(Camera.main.gameObject);
            TweenTo(Camera.main.gameObject, newCameraPosition);
            overheadCamera.distance = joystickMovement.transform.position - newCameraPosition.transform.position;
            tweenTimer = 0f;
            overheadCamera.SetCameraPosition(newCameraPosition.position);
            sidescrollCamera.enabled = false;
            joystickMovement.isSidescrolling = false;
        }
    }

    private void SetCameraActive()
    {
        overheadCamera.enabled = true;
    }

    private void TweenTo(GameObject go, Transform newPosition)
    {
        Debug.Log(newPosition.position);
        Debug.Log(newPosition.rotation.eulerAngles);
        LeanTween.move(go, newPosition.transform.position, timeTaken).setOnComplete(SetCameraActive);
        LeanTween.rotate(go, newPosition.rotation.eulerAngles, timeTaken).setOnComplete(SetCameraActive);
    }
}
