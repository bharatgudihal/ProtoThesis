using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : Mod
{

    [SerializeField, Range(10f, 1000f)]
    float thrustForce;
    bool canActivate = true;


    public override void Activate()
    {
        if (canActivate)
        {
            canActivate = false;
            Vector3 forceDirection = Camera.main.transform.TransformDirection(-transform.parent.forward * thrustForce);
            joystickMovement.AddJetForce(forceDirection, myModSpot);            
        }
    }

    void ReActivate()
    {
        canActivate = true;
    }

    public override void DeActivate()
    {
        canActivate = true;
        joystickMovement.StopConstantForce(myModSpot);
    }

    public override void Fatigue()
    {

    }
}

