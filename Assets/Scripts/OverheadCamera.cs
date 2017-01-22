using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadCamera : MonoBehaviour {

    private JoystickMovement player;
    public Vector3 distance;
    public Vector3 angle;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<JoystickMovement>();
        distance = player.transform.position - this.transform.position;
    }
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position - distance;
    }

    public void SetCameraPosition(Vector3 position)
    {
        distance = player.transform.position - position;
    }
}
