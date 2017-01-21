using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGun : Gun 
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
			Quaternion rotation = transform.rotation * i_CharObject.transform.rotation;
			Instantiate (bullet, transform.position, i_CharObject.transform.rotation);
			StartCoroutine (Recoil ());
		}
	}
}
