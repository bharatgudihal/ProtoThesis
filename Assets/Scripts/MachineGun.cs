using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Mod
{

	[SerializeField, Range(10f,1000f)] float kickbackForce; 
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
			Vector3 forceDirection = Camera.main.transform.TransformDirection(- i_RootObject.transform.forward * kickbackForce);
			joystickMovement.AddExternalForce(forceDirection);

			TimeCounting = 0.0f;
			GameObject instance = Instantiate (bullet, transform.position, i_RootObject.transform.rotation) as GameObject;
			instance.transform.rotation = i_RootObject.transform.rotation * Quaternion.Euler (-90, 0, 0);
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
