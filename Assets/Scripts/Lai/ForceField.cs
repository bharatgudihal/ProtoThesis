using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour 
{
	public bool IsActive = true;

	private KeyCode input = KeyCode.Z;
	private GameObject m_Field;

	// Use this for initialization
	void Start () 
	{
		m_Field = transform.GetChild (1).gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (input) && IsActive == true) 
			m_Field.SetActive (true);
		else 
			m_Field.SetActive (false);
	}

	void OnTriggerEnter(Collider other)
	{
		if(m_Field.activeSelf == true)
			Destroy (other.gameObject);
	}
}
