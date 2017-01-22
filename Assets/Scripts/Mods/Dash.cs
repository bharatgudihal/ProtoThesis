using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Mod {

    [SerializeField, Range(10f, 1000f)] float force;
    bool canActivate = true;
    [SerializeField] float cooldownTime;

    public override void Activate()
    {
        if (canActivate)
        {
            Vector3 forceDirection = Vector3.zero;
            switch (myModSpot)
            {

                case ModSpot.Down:
                    forceDirection = Camera.main.transform.TransformDirection(-transform.parent.forward * force);
                    break;
                case ModSpot.Left:
                case ModSpot.Right:
                    float h = Input.GetAxis("Horizontal");
                    float v;

                    if (joystickMovement.isSidescrolling)
                    {
                        v = 0f;
                    }
                    else
                    {
                        v = Input.GetAxis("Vertical");
                    }
                    forceDirection = new Vector3(h, 0f, v);
                    forceDirection *= force;
                    forceDirection = Camera.main.transform.TransformDirection(forceDirection);

                    break;
            }

            canActivate = false;
            Invoke("ReActivate", cooldownTime);
            forceDirection = new Vector3(forceDirection.x, 0f, forceDirection.z);
            joystickMovement.AddExternalForce(forceDirection);
        }
    }

    public override void Fatigue()
    {

    }

    void ReActivate()
    {
        canActivate = true;
    }

}
