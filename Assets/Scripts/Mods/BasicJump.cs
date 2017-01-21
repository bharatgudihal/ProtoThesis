using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicJump : Mod {


    private Rigidbody rigid;

    public float speed;
    public float gravity;
    public float jumpForce;
    public Vector3 velocity;

    public bool noAxisInput;
    public bool isGrounded;

    public float distToGround;

    private bool usedDoubleJump;
    private float gravityY;
    private float rigidY;
    private RaycastHit hitInfo;

    public override void Activate()
    {
        throw new NotImplementedException();
    }

    void Awake ()
    {
        rigid = GetComponent<Rigidbody>();
        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
    }

    // Use this for initialization
    void Start () {
        rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y + gravity * Time.deltaTime, rigid.velocity.z);
    }
	
	// Update is called once per frame
	void Update () {
        rigidY = rigid.velocity.y;
        isGrounded = IsGrounded();
        velocity = rigid.velocity;

        if (isGrounded)
        {

        }
        else
        {
            rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y + gravity * Time.deltaTime, rigid.velocity.z);
            rigidY = rigid.velocity.y;
            // rigid.velocity = movement * speed * Time.deltaTime;
            rigid.velocity = new Vector3(rigid.velocity.x, rigidY, rigid.velocity.z);

        }
    }

    public bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector3.up, Color.green);
        return (Physics.Raycast(transform.position, -Vector3.up, out hitInfo, distToGround + 0.005f) && rigid.velocity.y <= 0.01f);
    }
}
