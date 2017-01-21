using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonXRay : MonoBehaviour {

    Camera playerCamera;
    GameObject objectLookingAt;

    // Use this for initialization
    void Start () {
        playerCamera = Camera.main;
        objectLookingAt = null;
    }
	
	// Update is called once per frame
	void Update () {        
        RaycastHit hit;
        if (Input.GetButton("Up"))
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit)) {
                if (objectLookingAt != null)
                {
                    if (objectLookingAt == hit.transform.gameObject)
                    {
                        objectLookingAt = hit.transform.gameObject;
                        Color color = objectLookingAt.GetComponent<MeshRenderer>().material.color;
                        objectLookingAt.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, Mathf.Lerp(color.a, 0f, 0.1f));
                    }
                    else
                    {
                        Color color = objectLookingAt.GetComponent<MeshRenderer>().material.color;
                        objectLookingAt.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 1f);
                        objectLookingAt = hit.transform.gameObject;
                        color = objectLookingAt.GetComponent<MeshRenderer>().material.color;
                        objectLookingAt.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, Mathf.Lerp(color.a, 0f, 0.1f));
                    }
                }else
                {
                    objectLookingAt = hit.transform.gameObject;
                    Color color = objectLookingAt.GetComponent<MeshRenderer>().material.color;
                    objectLookingAt.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, Mathf.Lerp(color.a, 0f, 0.1f));
                }
            }
        }
        if (Input.GetButtonUp("Up"))
        {
            Color color = objectLookingAt.GetComponent<MeshRenderer>().material.color;
            objectLookingAt.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 1f);
        }
	}
}
