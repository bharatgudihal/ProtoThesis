using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

    [SerializeField] Rigidbody rigbod;
    [SerializeField, Range(0f, 10f)] float xPeriod;
    [SerializeField, Range(0f, 10f)] float zPeriod;
    [SerializeField, Range(1f, 20f)] float xRadius;
    [SerializeField, Range(1f, 20f)] float zRadius;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float x = Mathf.Cos((2 * Mathf.PI / xPeriod) * Time.time) * xRadius;
        float z = Mathf.Sin((2 * Mathf.PI / zPeriod) * Time.time) * zRadius;
        rigbod.velocity = new Vector3(x,0f,z);

    }
}
