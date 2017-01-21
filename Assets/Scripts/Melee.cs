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
            moveDir = new Vector3(moveDir.x, 0f,moveDir.z);

            player.Dash(moveDir * forwardForce);
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
        player.EnableMovement();
    }
}
