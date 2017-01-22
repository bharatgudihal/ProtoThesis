using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour 
{
	public bool IsTrigger = false;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter()
	{
		IsTrigger = true;
	}

	void OnTriggerExit()
	{
		IsTrigger = false;
	}
}
