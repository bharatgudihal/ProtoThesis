using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public GameObject bullet;
	public bool IsActive;
	public float Frequency;
	public float RecoilForce = 200;

	protected float TimeCounting = 0;
	protected KeyCode input = KeyCode.Space;

	public virtual void Mode ()
	{
		
	}

	protected IEnumerator Recoil()
	{
		GetComponent<Rigidbody> ().AddForce (transform.forward * -RecoilForce);
		yield return new WaitForSeconds(2);
		GetComponent<Rigidbody> ().AddForce (transform.forward *  RecoilForce);
	}
}
