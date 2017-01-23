using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour 
{
	public GameObject[] TriggerObjects; 
	protected PressurePad[] pad;

    protected bool IsActive = false;

    void Awake()
	{
		
	}

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{

	}
		
	public virtual void Action()
	{
		
	}
}
