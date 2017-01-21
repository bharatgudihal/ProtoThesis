using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadCamera : MonoBehaviour {

    private JoystickMovement player;
    float distance;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<JoystickMovement>();
        distance = Vector3.Distance(player.transform.position, this.transform.position);
    }
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - distance);
    }
}
