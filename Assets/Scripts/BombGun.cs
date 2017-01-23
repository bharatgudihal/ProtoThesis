using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGun : Mod 
{
    [SerializeField, Range(10f, 1000f)] float kickbackForce;
    public float Frequency;
    public GameObject bullet;
    private float TimeCounting = 0;
    private GameObject i_RootObject;

    void Start()
    {
        i_RootObject = transform.root.gameObject;
    }

    public override void Activate()
    {
        if (TimeCounting <= Frequency)
        {
            TimeCounting += Time.deltaTime;
        }
        else
        {
            Vector3 forceDirection = Camera.main.transform.TransformDirection(-i_RootObject.transform.forward * kickbackForce);
            joystickMovement.AddExternalForce(forceDirection);
            TimeCounting = 0.0f;
            Quaternion rotation = transform.rotation * i_RootObject.transform.rotation;
            Instantiate(bullet, transform.position, i_RootObject.transform.rotation);
        }
    }


    public override void DeActivate()
    {
        base.DeActivate();

    }

    public override void Fatigue()
    {

    }
}
