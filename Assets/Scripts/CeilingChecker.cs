using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingChecker : MonoBehaviour {

    GameObject currentCeiling;

	// Use this for initialization
	void Start () {
        currentCeiling = null;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);            
        if (hit.transform.tag.Equals("Ceiling"))
        {
            if (currentCeiling == null)
            {
                currentCeiling = hit.transform.gameObject;
                Color color = currentCeiling.GetComponent<MeshRenderer>().material.color;
                currentCeiling.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0f);
            }else if (currentCeiling.transform != hit.transform)
            {
                Color color = currentCeiling.GetComponent<MeshRenderer>().material.color;
                currentCeiling.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 1f);
                currentCeiling = hit.transform.gameObject;
                color = currentCeiling.GetComponent<MeshRenderer>().material.color;
                currentCeiling.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0f);
            }
        }
	}
}
