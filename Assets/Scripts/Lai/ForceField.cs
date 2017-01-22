using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : Mod 
{

	private GameObject m_Field;

	// Use this for initialization
	void Start () 
	{
		m_Field = transform.GetChild (1).gameObject;
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other);
		if(m_Field.activeSelf == true && isAttached && other.tag == "projectile")
			Destroy (other.gameObject);
	}

	public override void Activate()
	{		
		base.Activate ();
		m_Field.SetActive (true);
	}

	public override void DeActivate()
	{
		base.DeActivate ();
		m_Field.SetActive (false);
	}

	public override void Fatigue()
	{

	}
}
