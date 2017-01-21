using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_destroy : MonoBehaviour
{
	public float destroy_time = 2.0f;
	// Use this for initialization
	void Start ()
    {
		Destroy(gameObject, destroy_time);
    }
	
	// Update is called once per frame
	void Update ()
    {

	}
}
