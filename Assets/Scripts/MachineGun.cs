using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Mod
{
	public GameObject bullet;
	public float Frequency;

	private float TimeCounting = 0;
	private GameObject i_RootObject;

	void Start()
	{
		i_RootObject = transform.root.gameObject;
	}


	public override void Activate()
	{		
		if (TimeCounting <= Frequency) 
		{
			TimeCounting += Time.deltaTime;
		}
		else 
		{
			TimeCounting = 0.0f;
			GameObject instance = Instantiate (bullet, transform.position, i_RootObject.transform.rotation) as GameObject;
//			StartCoroutine (Recoil ());
		}
	}

	public override void DeActivate()
	{
		base.DeActivate ();
	}

	public override void Fatigue()
	{

	}
}
