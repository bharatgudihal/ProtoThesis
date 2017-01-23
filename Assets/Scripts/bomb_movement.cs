using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_movement : MonoBehaviour 
{
	public float velocity_x = 300.0f;
	public float velocity_y = 1000.0f;

	private Rigidbody m_Rigidbody;

	// Use this for initialization
	void Start ()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Rigidbody.AddForce(transform.forward * velocity_x);
		m_Rigidbody.AddForce(transform.up * velocity_y);

	}

	// Update is called once per frame
	void Update ()
	{
        transform.rotation = Quaternion.Euler(new Vector3(m_Rigidbody.velocity.normalized.y * -90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 2f));
	}
}
