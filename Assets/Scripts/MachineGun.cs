using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Gun 
{
	void Update () 
	{
		if (Input.GetKey (KeyCode.Space) && IsActive == true) 
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

			GameObject instance = Instantiate (bullet, transform.position, Quaternion.identity) as GameObject;
		}
	}
}
