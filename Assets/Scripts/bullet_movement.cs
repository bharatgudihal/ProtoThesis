using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_movement : MonoBehaviour
{
    public float velocity = 25.0f;
    private Rigidbody m_Rigidbody;
    // Use this for initialization

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
        
    }

    void OnEnable()
    {
        //        m_Rigidbody.AddForce(transform.forward * velocity);
        m_Rigidbody.velocity = transform.forward * velocity;
    }

    void OnDisable()
    {
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        //transform.Translate(velocity * Vector3.forward * Time.fixedDeltaTime);
	}
}
