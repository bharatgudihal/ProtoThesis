using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlaster : InteractiveObject 
{
	GameObject m_ParticleObject;

	// Use this for initialization
	void Awake()
	{
		pad = new PressurePad[TriggerObjects.Length];
	}

	void Start () 
	{
		m_ParticleObject = transform.GetChild (0).gameObject;

		for (int i = 0; i < pad.Length; i++) 
		{
			pad[i] = TriggerObjects[i].GetComponent<PressurePad> ();
		}
	}

	// Update is called once per frame
	void Update () 
	{
		bool success = true;

		for (int i = 0; i < pad.Length; i++) 
		{
			if (pad[i].IsTrigger == false) 
			{
				success = false;
			}
		}

		if (success == true) 
		{
			Action ();
		}
	}

	public override void Action()
	{
		m_ParticleObject.SetActive (true);
	}
}
