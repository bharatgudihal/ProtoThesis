using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_movement : MonoBehaviour
{
    public float velocity = 300.0f;
    private Rigidbody m_Rigidbody;
	// Use this for initialization

	void Start ()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_Rigidbody.AddForce(transform.forward * velocity);
	}
}
