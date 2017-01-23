using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sin : MonoBehaviour {

	[SerializeField] Rigidbody rigbod;
    [SerializeField, Range(1f, 20f)] float paceLength;
    [SerializeField, Range(0f, 10f)] float zVelocity;
    [SerializeField, Range(1f, 20f)] float yPeriod;
    [SerializeField, Range(1f, 5f)] float yAmplitude;

    Vector3 startPosition;
	// Use this for initialization
	void Start () {
        startPosition = transform.position;
	}

    // Update is called once per frame
    bool isPausingToReverse;
	void Update () {
        float yDistance = Mathf.Abs(transform.position.y - startPosition.y);
        float distanceAwayFromStart = Vector3.Distance(transform.position, startPosition) - yDistance;
        if (distanceAwayFromStart > paceLength && !isPausingToReverse) {
            ReverseDirection();
        }        
        float yVelocity = Mathf.Sin((2 * Mathf.PI / yPeriod) * Time.time) * yAmplitude;
        rigbod.velocity = transform.forward *zVelocity + Vector3.up * yVelocity;

    }

    void ReverseDirection() {
        zVelocity = -zVelocity;
        isPausingToReverse = true;
        Invoke("UnPauseForReverse", 0.1f);
    }

    void UnPauseForReverse() {
        isPausingToReverse = false;
    }
}
