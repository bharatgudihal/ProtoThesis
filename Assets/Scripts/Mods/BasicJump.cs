using System;
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
        joystickMovement = transform.root.GetComponent<JoystickMovement>();
        capsule = transform.root.GetComponent<CapsuleCollider>();
        rigid = transform.root.GetComponent<Rigidbody>();
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
        //Debug.DrawRay(capsule.transform.position, capsule.transform.position - Vector3.up, Color.red);
        // Ray ray = new Ray(capsule.transform.position, -Vector3.up);
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        return (Physics.CheckSphere(capsule.bounds.min + new Vector3(capsule.bounds.extents.x, 0f, capsule.bounds.extents.z), 0.1f, layerMask, QueryTriggerInteraction.Ignore) && rigid.velocity.y <= 0.01f);
        // return Physics.CheckSphere(capsule.bounds.center, new Vector3(capsule.bounds.center.x, capsule.bounds.min.y - 0.1f, capsule.bounds.center.z), capsule.radius, LayerMask.NameToLayer("Default"));
        // return (Physics.Raycast(ray, distToGround + 0.005f, (int)Layers.Default, QueryTriggerInteraction.Collide) && rigid.velocity.y <= 0.01f);
    }

    private void OnDrawGizmos()
    {
        if (capsule != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(capsule.bounds.min + new Vector3(capsule.bounds.extents.x, 0f, capsule.bounds.extents.z), 0.1f);
        }
    }

    public override void DeActivate()
    {
        throw new NotImplementedException();
    }

    public override void Fatigue()
    {
        throw new NotImplementedException();
    }
}

public enum Layers {
    Default=0,
    ModMan = 8
}


