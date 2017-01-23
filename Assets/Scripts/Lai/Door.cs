using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
	public float maxHeight = 120.0f;
	public float speed = 1.0f;
	private GameObject MovePart;
	private bool IsOpening = false;

	// Use this for initialization
	void Start () 
	{
        MovePart = gameObject.transform.GetChild(1).gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter()
	{
		if(IsOpening == false)
			StartCoroutine(openDoor());
	}

	IEnumerator openDoor()
	{
		IsOpening = true;

		for (float i = 0; i < maxHeight; i += 1.0f) 
		{
            MovePart.transform.Translate (Vector3.up * speed * Time.fixedDeltaTime);
			yield return 0;
		}

		yield return new WaitForSeconds(2);

		for (float i = 0; i < maxHeight; i += 1.0f) 
		{
            MovePart.transform.Translate (Vector3.down * speed * Time.fixedDeltaTime);
			yield return 0;
		}

		IsOpening = false;
    }

}
