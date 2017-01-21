using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour {

    [SerializeField] Rigidbody rigbod;
    [SerializeField, Range(5f, 25f)] float maxSpeed;
    [SerializeField, Range(.3f, 5f)] float acceleration;
    [SerializeField, Range(1f, 20f)] float rotationSpeed;

    void Awake() {
        StartCoroutine(Accelerate());
    }

    IEnumerator Accelerate() {
        float startTime = Time.time;
        while (rigbod.velocity.magnitude < maxSpeed) {
            float timeDif = Time.time - startTime;
            float speed = Mathf.Exp(timeDif)* acceleration;
            rigbod.velocity = transform.forward * speed;
            yield return null;
        }
        rigbod.velocity = Vector3.zero;
        StartCoroutine(TurnAround());
    }

    IEnumerator TurnAround()
    {
        float localStartAngle = transform.eulerAngles.y;
        float localEndAngle = localStartAngle + 180f;
        float x = transform.eulerAngles.x;
        float z = transform.eulerAngles.z;

        while (Mathf.Abs(transform.eulerAngles.y - localStartAngle) < 178f)
        {
            float yRotation = transform.eulerAngles.y + rotationSpeed;
            transform.rotation = Quaternion.Euler(x, yRotation, z);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(x, localEndAngle, z);
        StartCoroutine(Accelerate());
    }
}
