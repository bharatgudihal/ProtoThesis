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

    private Rigidbody rigid;

    public float speed;
    public float gravity;
    public float jumpForce;
    public Vector3 velocity;

    public bool noAxisInput;
    public bool isGrounded;

    public float distToGround;

    private bool canMove=true;
    private bool usedDoubleJump;
    private float gravityY;
    private float rigidY;
    private RaycastHit hitInfo;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        state = PlayerState.JUMPING;
        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
    }

    // Use this for initialization
    void Start()
    {

        rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y + gravity * Time.deltaTime, rigid.velocity.z);
    }

    public void EnableMovement() {
        canMove = true;
    }

    public void Dash(Vector3 moveVec) {
        rigid.AddForce(moveVec);
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(h, 0.0f, v);
        if (!canMove) {
            movement = Vector3.zero;
        }
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
                    isGrounded = false;
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
                    isGrounded = false;
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
                    usedDoubleJump = false;
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
        Debug.DrawRay(transform.position, -Vector3.up, Color.green);
        return (Physics.Raycast(transform.position, -Vector3.up, out hitInfo, distToGround + 0.005f) && rigid.velocity.y <= 0.01f);
    }
}
