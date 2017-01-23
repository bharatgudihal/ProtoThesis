using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour {


    [SerializeField] Transform foot;
    [SerializeField] float speed;
    public bool noAxisInput;
    public bool isSidescrolling;

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
    void Start() {
        externalForces = new List<Vector3>();
        for (int i = 0; i < 100; i++) {
            externalForces.Add(Vector3.zero);
        }

    }
    Vector3 movement;

    private bool canMove = true;
    // Update is called once per frame
    void Update() {

        float h = Input.GetAxis("Horizontal");

        float v;
        if (isSidescrolling)
        {
            v = 0f;
        }
        else
        {
            v = Input.GetAxis("Vertical");
        }
        movement = new Vector3(h, 0.0f, v);
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

        isGrounded = IsGrounded();
    }

    private void FixedUpdate()
    {
        rigid.velocity = movement * speed * Time.fixedDeltaTime + GetExternalForceSum();
    }

    Vector3 GetExternalForceSum() {
        Vector3 totalExternalForce = Vector3.zero;
        externalForces.ForEach(force => totalExternalForce += force);
        return totalExternalForce;
    }

    public void AddExternalForce(Vector3 forceVector, float decay = 0.1f) {
        if (canMove) {
            StartCoroutine(AddPsuedoForce(forceVector, decay));
        }
    }

    public void AddJetForce(Vector3 constantForce, ModSpot modSpot) {
        modSpotConstantForceIndices[modSpot] = true;
        if (modSpot == ModSpot.Down) {
            isFalling = true;
        }
        StartCoroutine(ApplyJetForce(constantForce, modSpot));
    }
    public void StopConstantForce(ModSpot modSpot) {
        modSpotConstantForceIndices[modSpot] = false;
        if (modSpot == ModSpot.Down) {
            isFalling = false;
        }
    }

    Dictionary<ModSpot, bool> modSpotConstantForceIndices = new Dictionary<ModSpot, bool>() {
        {ModSpot.Up, false},
        {ModSpot.Down, false},
        {ModSpot.Left, false},
        {ModSpot.Right, false}
    };
    IEnumerator ApplyJetForce(Vector3 constantForce, ModSpot modSpot) {
        int currentIndex = externalForces.FindIndex(vec => vec == Vector3.zero);        
        while (modSpotConstantForceIndices[modSpot] && externalForces[currentIndex].magnitude < constantForce.magnitude - 0.1f) {
            externalForces[currentIndex] = Vector3.Lerp(externalForces[currentIndex], constantForce, 0.1f);
            yield return null;
        }
        StartCoroutine(FadeTargetForce(currentIndex));

        float period = 5f;
        float timePassed = 0f;
        currentIndex = externalForces.FindIndex(vec => vec == Vector3.zero);
        while (modSpotConstantForceIndices[modSpot]) {
            externalForces[currentIndex] = 0.3f*constantForce * -Mathf.Sin((Mathf.PI*2/period) * timePassed);
            timePassed += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(FadeTargetForce(currentIndex));
    }

    IEnumerator FadeTargetForce(int targetIndex) {
        while (externalForces[targetIndex].magnitude > 0.1f) {
            externalForces[targetIndex] = Vector3.Lerp(externalForces[targetIndex], Vector3.zero, 0.1f);
            yield return null;
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

    IEnumerator ApplyGravity() {
        isFalling = true;
        int currentIndex = externalForces.FindIndex(vec => vec == Vector3.zero);
        float timeElapsed = 0f;
        float gravity = 9.81f;
        while (!isGrounded && isFalling) {
            externalForces[currentIndex] = Vector3.down * (gravity * timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        isFalling = false;
        externalForces[currentIndex] = Vector3.zero;
    }

    RaycastHit hitInfo;
    bool isGrounded;
    bool isFalling=false;
    float sphereRadius = 0.1f;
    public bool IsGrounded()
    {
        Collider[] cols = Physics.OverlapSphere(foot.transform.position, sphereRadius);
        for (int i = 0; i < cols.Length; i++) {
            if (cols[i].gameObject.layer != (int)Layers.ModMan) {
                return true;
            }
        }
        if (!isFalling) {
            StartCoroutine(ApplyGravity());
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(foot.transform.position, sphereRadius);
    }
}
