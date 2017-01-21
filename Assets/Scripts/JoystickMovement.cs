using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour {

    

    public float speed;
    public bool noAxisInput;

    #region Privates

    [SerializeField]
    private Vector3 velocity;
    private Rigidbody rigid;
    private float rigidY;
    #endregion

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void EnableMovement() {
        canMove = true;
    }

    public void Dash(Vector3 moveVec) {
        rigid.AddForce(moveVec);
        canMove = false;
    }

    // Use this for initialization
    void Start () {
		
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
        rigid.velocity = movement * speed * Time.deltaTime;
        rigid.velocity = new Vector3(rigid.velocity.x, rigidY, rigid.velocity.z);

    }
}
