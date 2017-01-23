using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : Mod 
{
    [SerializeField, Range(10f, 1000f)]float kickbackForce;

    private GameObject m_Field;
    private GameObject i_RootObject;

    // Use this for initialization
    void Start () 
	{
        i_RootObject = transform.root.gameObject;
        m_Field = transform.GetChild (1).gameObject;
	}

	void OnTriggerEnter(Collider other)
	{
        if (m_Field.activeSelf == true && isAttached && other.tag == "projectile")
        {
            Vector3 forceDirection = Camera.main.transform.TransformDirection(-i_RootObject.transform.forward * kickbackForce);
            if (other.GetComponent<Rigidbody>()) {
                forceDirection = other.GetComponent<Rigidbody>().velocity.normalized * kickbackForce;
            }
            joystickMovement.AddExternalForce(forceDirection);
            other.gameObject.SetActive(false);
        }
	}

	public override void Activate()
	{		
		base.Activate ();
		m_Field.SetActive (true);
	}

	public override void DeActivate()
	{
		base.DeActivate ();
		m_Field.SetActive (false);
	}

	public override void Fatigue()
	{

	}
}
