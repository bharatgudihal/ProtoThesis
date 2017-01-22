using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : InteractiveObject 
{
	public GameObject i_Bullet;
//    bool IsActive = false;

    public float Frequency;
    private float TimeCounting = 0;

    // Use this for initialization
    void Awake()
	{
		pad = new PressurePad[TriggerObjects.Length];
	}

	void Start () 
	{
		for (int i = 0; i < pad.Length; i++) 
		{
			pad[i] = TriggerObjects[i].GetComponent<PressurePad> ();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
        bool success = true;

        if (IsActive == false)
        {
            for (int i = 0; i < pad.Length; i++)
            {
                if (pad[i].IsTrigger == false)
                {
                    success = false;
                }
            }
        }

		if (success == true) 
		{
            IsActive = true;
            success = false;


            if (TimeCounting <= Frequency)
            {
                TimeCounting += Time.deltaTime;
            }
            else
            {
                TimeCounting = 0.0f;
                Action();
            }
		}
	}

	public override void Action()
	{
//		GameObject instance = Instantiate (i_Bullet, transform.position, transform.rotation) as GameObject;

		GameObject instance = BulletPool.instance.GetBullet ();

        if (instance != null)
        {
            instance.SetActive(true);
            instance.tag = "projectile";
            instance.transform.rotation = transform.rotation;
            instance.transform.position = transform.position;
        }
	}
}
