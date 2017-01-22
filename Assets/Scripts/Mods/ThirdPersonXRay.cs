using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonXRay : Mod
{

    Camera playerCamera;
    GameObject objectLookingAt;

    [SerializeField]
    float decayRate;

    // Use this for initialization
    void Start()
    {
        playerCamera = Camera.main;
        objectLookingAt = null;
    }
    public override void Activate()
    {
        RaycastHit[] hits = Physics.RaycastAll(playerCamera.transform.position, playerCamera.transform.forward);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.tag == "Wall")
            {
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
                }
                else
                {
                    objectLookingAt = hit.transform.gameObject;
                    Color color = objectLookingAt.GetComponent<MeshRenderer>().material.color;
                    objectLookingAt.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, Mathf.Lerp(color.a, 0f, 0.1f));
                }
            }
        }
    }

    public override void DeActivate()
    {
        if (objectLookingAt!=null) {
            Color color = objectLookingAt.GetComponent<MeshRenderer>().material.color;
            objectLookingAt.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 1f);
        }
    }

    public override void Fatigue()
    {
        health -= decayRate;
    }
}
