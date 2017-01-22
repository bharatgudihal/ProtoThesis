using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Mod {
    
    [SerializeField] Collider hitBox;
    [SerializeField, Range(0f,2f)] float timeHitBoxIsActive;
    [SerializeField, Range(0f,2f)] float timeBetweenStrikes;
    [SerializeField, Range(500f,10000f)] float forwardForce;

	// Use this for initialization
	void Start () {
        hitBox.enabled = false;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
	}

    bool canActivate=true;
    public override void Activate() {
        if (canActivate) {
            canActivate = false;
            Vector3 moveDir = Camera.main.transform.TransformDirection(transform.forward);
            if (myModSpot == ModSpot.Up || myModSpot == ModSpot.Down) {
                moveDir = transform.forward;
            }

            joystickMovement.Dash(moveDir * forwardForce);
            hitBox.enabled = true;
            Invoke("TurnOffHitBox", timeHitBoxIsActive);
            Invoke("ReactivateMovement", timeBetweenStrikes);
        }
    }

    void TurnOffHitBox() {
        hitBox.enabled = false;
    }    
    void ReactivateMovement() {
        canActivate = true;
        joystickMovement.EnableMovement();
    }

    public override void DeActivate()
    {
        throw new NotImplementedException();
    }

    public override void Fatigue()
    {
        throw new NotImplementedException();
    }
}
