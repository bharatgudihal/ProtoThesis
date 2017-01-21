using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricXRay : MonoBehaviour {

    Camera playerCamera;
    Quaternion rayCastForwardAngle;
    Quaternion rayCastRightAngle;
    Quaternion rayCastLeftAngle;
    GameObject ceilingForward;
    GameObject ceilingRight;
    GameObject ceilingLeft;
    GameObject currentCeiling;

    // Use this for initialization
    void Start () {
        playerCamera = Camera.main;
        rayCastForwardAngle = Quaternion.AngleAxis(-30, Vector3.forward);
        rayCastRightAngle = Quaternion.AngleAxis(90, Vector3.up);
        rayCastLeftAngle = Quaternion.AngleAxis(-90, Vector3.up); ;
        ceilingForward = null;
        ceilingRight = null;
        currentCeiling = null;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(playerCamera.transform.position, rayCastLeftAngle * playerCamera.transform.forward, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit))
        {
            if (hit.transform.tag == "Ceiling")
            {
                currentCeiling = hit.transform.gameObject;                
            }
        }
        if (Input.GetButton("Up"))
        {
            if (Physics.Raycast(playerCamera.transform.position, rayCastForwardAngle * playerCamera.transform.forward, out hit))
            {
                if (hit.transform.tag == "Ceiling")
                {                    
                    if (currentCeiling != hit.transform.gameObject)
                    {
                        ceilingForward = hit.transform.gameObject;
                        Color color = ceilingForward.GetComponent<MeshRenderer>().material.color;
                        ceilingForward.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, Mathf.Lerp(color.a, 0f, 0.01f));
                    }
                }
            }
            if (Physics.Raycast(playerCamera.transform.position, rayCastRightAngle * playerCamera.transform.forward, out hit))
            {
                if (hit.transform.tag == "Ceiling")
                {
                    if (currentCeiling != hit.transform.gameObject)
                    {
                        ceilingRight = hit.transform.gameObject;
                        Color color = ceilingRight.GetComponent<MeshRenderer>().material.color;
                        ceilingRight.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, Mathf.Lerp(color.a, 0f, 0.01f));
                    }
                }
            }
            if (Physics.Raycast(playerCamera.transform.position, rayCastLeftAngle * playerCamera.transform.forward, out hit))
            {
                if (hit.transform.tag == "Ceiling")
                {
                    if (currentCeiling != hit.transform.gameObject)
                    {
                        ceilingLeft = hit.transform.gameObject;
                        Color color = ceilingLeft.GetComponent<MeshRenderer>().material.color;
                        ceilingLeft.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, Mathf.Lerp(color.a, 0f, 0.01f));
                    }
                }
            }
        }
        if (Input.GetButtonUp("Up"))
        {
            Color color;
            if (ceilingForward != null)
            {
                color = ceilingForward.GetComponent<MeshRenderer>().material.color;
                ceilingForward.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 1f);
            }
            if (ceilingRight != null)
            {
                color = ceilingRight.GetComponent<MeshRenderer>().material.color;
                ceilingRight.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 1f);
            }
            if (ceilingLeft != null)
            {
                color = ceilingRight.GetComponent<MeshRenderer>().material.color;
                ceilingLeft.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 1f);
            }
            
        }
	}
}
