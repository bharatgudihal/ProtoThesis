using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_movement : MonoBehaviour 
{
	public float velocity_x = 100.0f;
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
		m_Rigidbody.AddForce(transform.up * velocity_x * parameter);
		parameter += 0.25f;
	}
}
