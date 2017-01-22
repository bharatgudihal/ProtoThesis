using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModManager : MonoBehaviour {

    Mod upMod;
    Mod leftMod;
    Mod rightMod;
    Mod downMod;
    Mod currentMod;
    JoystickMovement joystickMovement;
    Transform up;
    Transform down;
    Transform left;
    Transform right;

    // Use this for initialization
    void Start () {
        upMod = null;
        leftMod = null;
        rightMod = null;
        downMod = null;
        currentMod = null;
        joystickMovement = GetComponent<JoystickMovement>();
        Transform mods = transform.FindChild("Mods");
        foreach(Transform child in mods)
        {
            if(child.name == "Up")
            {
                up = child.transform;
            }
            if (child.name == "Down")
            {
                down = child.transform;
            }
            if (child.name == "Left")
            {
                left = child.transform;
            }
            if (child.name == "Right")
            {
                right = child.transform;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("RightBumper"))
        {
            if (currentMod != null)
            {
                if (Input.GetButtonDown("Up"))
                {
                    if (upMod != null)
                    {
                        DetachMod(upMod);
                    }
                    upMod = currentMod;
                    upMod.myModSpot = ModSpot.Up;
                    AttachMod(upMod);
                }
                if (Input.GetButtonDown("Down"))
                {
                    if (downMod != null)
                    {
                        DetachMod(downMod);
                    }
                    downMod = currentMod;
                    downMod.myModSpot = ModSpot.Down;
                    AttachMod(downMod);                    
                }
                if (Input.GetButtonDown("Left"))
                {
                    if (leftMod != null)
                    {
                        DetachMod(leftMod);
                    }
                    leftMod = currentMod;
                    leftMod.myModSpot = ModSpot.Left;
                    AttachMod(leftMod);
                }
                if (Input.GetButtonDown("Right"))
                {
                    if (rightMod != null)
                    {
                        DetachMod(rightMod);
                    }
                    rightMod = currentMod;
                    rightMod.myModSpot = ModSpot.Right;
                    AttachMod(rightMod);
                }
            }
        }
	}

    void DetachMod(Mod mod)
    {
        mod.Dettach();
        mod.transform.parent = null;
        //upMod.GetComponent<Rigidbody>().detectCollisions = true;
        mod.isEnabled = false;
        mod.GetComponent<CapsuleCollider>().enabled = true;
    }

    void AttachMod(Mod mod)
    {
        mod.isEnabled = true;
        mod.Attach(joystickMovement);
        mod.transform.gameObject.layer = (int)Layers.ModMan;
        mod.GetComponent<CapsuleCollider>().enabled = false;        
        switch (mod.myModSpot)
        {
            case ModSpot.Up:                
                mod.transform.parent = up;       
                break;
            case ModSpot.Down:
                mod.transform.parent = down;
                break;
            case ModSpot.Left:
                mod.transform.parent = left;
                break;
            case ModSpot.Right:
                mod.transform.parent = right;
                break;
        }
        mod.transform.position = Vector3.zero;
        mod.transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other);
        if(other.tag == "Mod")
        {
            currentMod = other.GetComponent<Mod>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentMod = null;
    }
}
