using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burster : MonoBehaviour {

	[SerializeField] Rigidbody rigbod;
    [SerializeField, Range(900f, 2000f)] float zForce;
    [SerializeField, Range(1f, 20f)] float rotationSpeed;
    [SerializeField, Range(.1f, 3f)] float waitTimeBeforeBurst;


    // Use this for initialization
	void Start () {
        StartCoroutine(Burst());
	}

    // Update is called once per frame
    IEnumerator Burst() {
        rigbod.AddForce(transform.forward * zForce);
        yield return new WaitForSeconds(0.2f);
        while (rigbod.velocity.magnitude>0.5f) {
            yield return null;
        }
        rigbod.velocity = Vector3.zero;
        StartCoroutine(TurnAround());
    }

    IEnumerator TurnAround() {
        float localStartAngle = transform.eulerAngles.y;
        float localEndAngle = localStartAngle + 180f;
        float x = transform.eulerAngles.x;
        float z = transform.eulerAngles.z;

        while (Mathf.Abs(transform.eulerAngles.y - localStartAngle) < 178f) {
            float yRotation = transform.eulerAngles.y + rotationSpeed;
            transform.rotation = Quaternion.Euler(x, yRotation, z);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(x, localEndAngle, z);
        yield return new WaitForSeconds(waitTimeBeforeBurst);
        StartCoroutine(Burst());
    }
}
