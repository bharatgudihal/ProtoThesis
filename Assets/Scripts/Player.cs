using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE = 0,
    RUNNING = 1,
    DASHING = 2,
    TAUNTING = 3,
    JUMPING = 4,
    AIRDASHING = 5
}


public class Player : MonoBehaviour
{

    public PlayerState state;

    public float speed;
    public float gravity;
    public float jumpForce;
    public Vector3 velocity;

    public bool noAxisInput;
    public bool isGrounded;

    public float distToGround;

    #region Privates
    private Rigidbody rigid;
    private float gravityY;
    private float rigidY;
    #endregion

    private void Awake()
    {
        rigid = GetComponentInChildren<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {
        state = PlayerState.JUMPING;
        distToGround = GetComponentInChildren<CapsuleCollider>().bounds.extents.y;
        rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y + gravity * Time.deltaTime, rigid.velocity.z);
    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(h, 0.0f, v);
        rigidY = rigid.velocity.y;
        isGrounded = IsGrounded();
        velocity = rigid.velocity;

        noAxisInput = false;

        if (Mathf.Approximately(h, 0f))
        {
            if (Mathf.Approximately(v, 0f))
            {
                noAxisInput = true;
            }

        }

        switch (state)
        {
            case PlayerState.IDLE:

                if (!noAxisInput)
                {
                    state = PlayerState.RUNNING;
                }

                movement = Camera.main.transform.TransformDirection(movement);
                movement.y = 0f;
                movement = movement.normalized;
                rigid.velocity = movement * speed * Time.deltaTime;
                rigid.velocity = new Vector3(rigid.velocity.x, 0f, rigid.velocity.z);


                if (Input.GetButtonDown("Jump"))
                {
                    state = PlayerState.JUMPING;
                    rigid.velocity = new Vector3(rigid.velocity.x, jumpForce, rigid.velocity.z);
                    break;
                }

                break;

            case PlayerState.RUNNING:

                if (noAxisInput)
                {
                    state = PlayerState.IDLE;
                    break;
                }

                movement = Camera.main.transform.TransformDirection(movement);
                movement.y = 0f;
                movement = movement.normalized;
                rigid.velocity = movement * speed * Time.deltaTime;
                rigid.velocity = new Vector3(rigid.velocity.x, 0f, rigid.velocity.z);



                if (Input.GetButtonDown("Jump"))
                {
                    state = PlayerState.JUMPING;
                    rigid.velocity = new Vector3(rigid.velocity.x, jumpForce, rigid.velocity.z);
                    break;
                }

                break;

            case PlayerState.JUMPING:
                rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y + gravity * Time.deltaTime, rigid.velocity.z);
                rigidY = rigid.velocity.y;

                if (isGrounded)
                {
                    if (noAxisInput) state = PlayerState.IDLE;
                    else state = PlayerState.RUNNING;
                    break;
                }


                movement = Camera.main.transform.TransformDirection(movement);
                movement.y = 0f;
                movement = movement.normalized;
                rigid.velocity = movement * speed * Time.deltaTime;
                rigid.velocity = new Vector3(rigid.velocity.x, rigidY, rigid.velocity.z);

                break;

            default:
                break;
        }
    }



    public bool IsGrounded()
    {
        return (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.005f) && velocity.y <= 0f);
    }
}