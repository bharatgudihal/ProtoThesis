using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : InteractiveObject 
{
	public GameObject i_Bullet;

	// Use this for initialization
	void Awake()
	{
		pad = new PressurePad[TriggerObjects.Length];
	}

	void Start () 
	{
		for (int i = 0; i < pad.Length; i++) 
		{
			pad[i] = TriggerObjects[i].GetComponent<PressurePad> ();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool success = true;

		for (int i = 0; i < pad.Length; i++) 
		{
			if (pad[i].IsTrigger == false) 
			{
				success = false;
			}
		}

		if (success == true) 
		{
			Action ();
		}
	}

	public override void Action()
	{
		GameObject instance = Instantiate (i_Bullet, transform.position, transform.rotation) as GameObject;
	}
}
