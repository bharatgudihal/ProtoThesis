using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour 
{
	public GameObject TriggerObject; 
	public GameObject i_Bullet;

	PressurePad pad;

	// Use this for initialization
	void Start () 
	{
		pad = TriggerObject.GetComponent<PressurePad> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (pad.IsTrigger == true) 
		{
			GameObject instance = Instantiate (i_Bullet, transform.position, transform.rotation) as GameObject;
		}
	}
}
