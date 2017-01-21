using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public GameObject bullet;
	public bool IsActive;
	public float Frequency;
	public float RecoilForce = 200;

	public GameObject i_CharObject;


	protected float TimeCounting = 0;
	protected KeyCode input = KeyCode.A;

	public virtual void Mode ()
	{
		
	}

	protected IEnumerator Recoil()
	{
		i_CharObject.GetComponent<Rigidbody> ().AddForce (i_CharObject.transform.forward * -RecoilForce);
		yield return new WaitForSeconds(2);
		i_CharObject.GetComponent<Rigidbody> ().AddForce (i_CharObject.transform.forward *  RecoilForce);
	}
}
