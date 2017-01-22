using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour {

    

    [SerializeField] float speed;
    public bool noAxisInput;

    #region Privates

    [SerializeField]
    public Vector3 velocity;
    private Rigidbody rigid;
    private float rigidY;
    #endregion

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
        externalForces = new List<Vector3>();
        for (int i = 0; i < 100; i++) {
            externalForces.Add(Vector3.zero);
        }

    }

    private bool canMove = true;
    // Update is called once per frame
    void Update () {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(h, 0.0f, v);
        if (!canMove) {
            movement = Vector3.zero;
        }
        rigidY = rigid.velocity.y;

        velocity = rigid.velocity;

        noAxisInput = false;

        if (Mathf.Approximately(h, 0f))
        {
            if (Mathf.Approximately(v, 0f))
            {
                noAxisInput = true;
            }

        }


        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f;
        movement = movement.normalized;

        rigid.velocity = movement * speed * Time.deltaTime + GetExternalForceSum();
        //rigid.velocity += new Vector3(rigid.velocity.x, rigidY, rigid.velocity.z);

    }

    Vector3 GetExternalForceSum() {
        Vector3 totalExternalForce = Vector3.zero;
        externalForces.ForEach(force => totalExternalForce += force);
        return totalExternalForce;
    }

    public void AddExternalForce(Vector3 forceVector, float decay=0.1f) {
        if (canMove) {
            StartCoroutine(AddPsuedoForce(forceVector, decay));
        }
    }

    List<Vector3> externalForces;
    IEnumerator AddPsuedoForce(Vector3 forceVector, float decay) {
        int currentIndex = externalForces.FindIndex(vec => vec == Vector3.zero);

        externalForces[currentIndex] = forceVector;
        while (externalForces[currentIndex].magnitude > .2f) {
            externalForces[currentIndex] = Vector3.Lerp(externalForces[currentIndex], Vector3.zero, decay);
            yield return null;
        }
        externalForces[currentIndex] = Vector3.zero;
    }
}
