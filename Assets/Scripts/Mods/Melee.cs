using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Mod {
    
    [SerializeField] Collider hitBox;
    [SerializeField, Range(0f,2f)] float timeHitBoxIsActive;
    [SerializeField, Range(0f,2f)] float timeBetweenStrikes;
    [SerializeField, Range(100f,1000f)] float forwardForce;

	// Use this for initialization
	void Start () {
        hitBox.enabled = false;
        type = ModTypes.SWORD;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
	}

    bool canActivate=true;
    public override void Activate() {
        if (canActivate) {
            canActivate = false;
            Vector3 moveDir = Camera.main.transform.TransformDirection(transform.parent.forward);
            if (myModSpot == ModSpot.Up || myModSpot == ModSpot.Down) {
                moveDir = transform.parent.forward;
            }

            joystickMovement.AddExternalForce(moveDir * forwardForce, 0.4f);
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
    }

    public override void DeActivate()
    {        
    }

    public override void Fatigue()
    {        
    }
}
