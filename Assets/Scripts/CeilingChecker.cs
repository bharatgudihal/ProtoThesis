using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingChecker : MonoBehaviour {

    GameObject previousCeiling;
    GameObject currentCeiling;

    [SerializeField]
    float fadeInSpeed;

    // Use this for initialization
    void Start () {
        previousCeiling = null;
        currentCeiling = null;
    }
	
	// Update is called once per frame
	void Update () {
        CheckCeilingThroughRayCast();
        StartFadingIn();
	}

    void CheckCeilingThroughRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.tag.Equals("Ceiling"))
            {
                if (currentCeiling == null)
                {
                    currentCeiling = hit.transform.gameObject;
                    Color color = currentCeiling.GetComponent<MeshRenderer>().material.color;
                    currentCeiling.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0f);
                }
                else if (currentCeiling.transform != hit.transform)
                {
                    previousCeiling = currentCeiling;
                    currentCeiling = hit.transform.gameObject;
                    Color color = currentCeiling.GetComponent<MeshRenderer>().material.color;
                    currentCeiling.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0f);
                }
            }
        }
    }

    void StartFadingIn()
    {
        if(previousCeiling != null)
        {
            Color color = previousCeiling.GetComponent<MeshRenderer>().material.color;
            if (color.a != 1f)
            {
                previousCeiling.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, Mathf.Lerp(color.a, 1f, fadeInSpeed));
            }
        }
    }
}
