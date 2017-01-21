using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun 
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

			for (int i = -4; i <= 4; i++) 
			{
				Quaternion rotation = Quaternion.Euler (0, 5 * i, 0) * transform.rotation;
				Instantiate (bullet, transform.localPosition, rotation);
			}
			StartCoroutine (Recoil ());
		}
	}
}
