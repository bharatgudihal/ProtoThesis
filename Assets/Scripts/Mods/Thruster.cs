using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : Mod {

    [SerializeField] Rigidbody rigid;
    [SerializeField, Range(10f,1000f)] float thrustForce; 
    bool canActivate=true;


    public override void Activate()
    {
        if (canActivate)
        {
            Vector3 forceDirection = Camera.main.transform.TransformDirection(transform.parent.forward * thrustForce);
            joystickMovement.AddExternalForce(forceDirection);
            canActivate = false;
            Invoke("ReActivate", .3f);
        }
    }

    void ReActivate() {
        canActivate = true;
    }


}
