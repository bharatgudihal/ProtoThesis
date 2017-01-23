using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_movement : MonoBehaviour 
{
	public float velocity = 100.0f;
	private Rigidbody m_Rigidbody;
	private float parameter = 1.0f;

	// Use this for initialization
	void Start ()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update ()
	{
        transform.Translate(velocity * Vector3.forward * Time.fixedDeltaTime);
    }
}
