using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Gun 
{
	void Update () 
	{
		if (Input.GetKey (input)  && IsActive == true) 
		{ 
			Mode();
		}
	}

	public override void Mode()
	{
		if (TimeCounting <= Frequency) 
		{
			TimeCounting += Time.deltaTime;  
		}
		else 
		{
			TimeCounting = 0.0f;
			GameObject instance = Instantiate (bullet, transform.position, transform.rotation) as GameObject;
			instance.transform.localEulerAngles = new Vector3 (90, 0, 0);
			StartCoroutine (Recoil ());
		}
	}
}