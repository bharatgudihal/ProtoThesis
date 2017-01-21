﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicJump : Mod {


    [SerializeField] Rigidbody rigid;
    [SerializeField] CapsuleCollider capsule;

    public float gravity;
    public float jumpForce;
    public Vector3 velocity;

    public bool noAxisInput;
    public bool isGrounded;

    public float distToGround;

    private float gravityY;
    private float rigidY;
    private RaycastHit hitInfo;

    public override void Activate()
    {
        if (isGrounded)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, jumpForce, rigid.velocity.z);
            isGrounded = false;
        }
    }

    void Awake ()
    {
        distToGround = capsule.bounds.extents.y;
    }


    // Use this for initialization
    void Start () {
        rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y + gravity * Time.deltaTime, rigid.velocity.z);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Down"))
        {
            Activate();
        }

        rigidY = rigid.velocity.y;
        isGrounded = IsGrounded();
        velocity = rigid.velocity;

        if (isGrounded)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, 0f, rigid.velocity.z);
        }
        else
        {
            rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y + gravity * Time.deltaTime, rigid.velocity.z);
            rigidY = rigid.velocity.y;
        }
    }

    public bool IsGrounded()
    {
        Debug.DrawRay(capsule.transform.position, capsule.transform.position - Vector3.up, Color.red);
        Ray ray = new Ray(capsule.transform.position, -Vector3.up);
        return (Physics.Raycast(ray, distToGround + 0.005f,(int)Layers.Default, QueryTriggerInteraction.Collide) && rigid.velocity.y <= 0.01f);
    }
}

public enum Layers {
    Default=0,
    ModMan = 8
}