using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public bool IsActive;
	public GameObject bullet;
	public float Frequency;
	protected float TimeCounting = 0;

	public virtual void Mode ()
	{
		
	}
}
