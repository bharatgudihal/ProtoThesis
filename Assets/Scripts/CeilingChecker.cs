using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingChecker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        if(hit.transform.tag.Equals("Ceiling"))
        {
            hit.transform.gameObject.SetActive(false);
        }
	}
}
