using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour 
{
	public GameObject i_Enemy;
	public float Frequency;
	protected float TimeCounting = 0.0f;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (TimeCounting <= Frequency) 
		{
			TimeCounting += Time.deltaTime;  
		}
		else 
		{
			TimeCounting = 0.0f;
			GameObject instance = Instantiate (i_Enemy, transform.position, transform.rotation) as GameObject;
		}
	}
}
