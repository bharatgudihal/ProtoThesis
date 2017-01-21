using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Mod {

    [SerializeField] Collider hitBox;

	// Use this for initialization
	void Start () {
        hitBox.enabled = false;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
	}

    bool canActivate=true;
    float forwardForce = 3000f;
    public override void Activate() {
        if (canActivate) {
            canActivate = false;
            Vector3 moveDir = Camera.main.transform.TransformDirection(transform.forward);
            moveDir = new Vector3(moveDir.x, 0f,moveDir.z);

            rigbod.AddForce(moveDir * forwardForce);
            hitBox.enabled = true;
            StartCoroutine(DeActivateMovement());
            Invoke("TurnOffHitBox", .3f);
            Invoke("ReactivateMovement", 1f);
        }
    }

    void ReactivateMovement() {
        canActivate = true;
    }

    void TurnOffHitBox() {
        hitBox.enabled = false;
    }

    IEnumerator DeActivateMovement() {
        while (!canActivate) {
            rigbod.velocity = Vector2.zero;
            yield return null;
        }
    }
}
