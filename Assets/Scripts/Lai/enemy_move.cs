using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move : MonoBehaviour 
{
	public GameObject i_target;
	public float speed = 3.0f;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, i_target.transform.position, step);
	}
}
