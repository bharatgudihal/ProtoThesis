using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Mod 
{
    [SerializeField, Range(10f, 1000f)]
    float kickbackForce;
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

            Vector3 forceUp = Camera.main.transform.TransformDirection(i_RootObject.transform.up * kickbackForce);
            joystickMovement.AddExternalForce(forceUp);

            TimeCounting = 0.0f;
            GameObject instance = Instantiate(bullet, transform.position, i_RootObject.transform.rotation) as GameObject;
 //           instance.transform.rotation = i_RootObject.transform.rotation * Quaternion.Euler(90, 0, 0);
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