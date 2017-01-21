using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    float magnitude;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float moveX = Input.GetAxis("Horizontal")* magnitude;
        float moveZ = Input.GetAxis("Vertical")* magnitude;
        transform.Translate(moveX, 0, moveZ);
    }
}
