using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePit : InteractiveObject
{
    private float openSpeed = 0.5f;
    private GameObject Shield_L;
    private GameObject Shield_R;
    private GameObject Core;
    private GameObject Fire;


    void Awake()
    {
        pad = new PressurePad[TriggerObjects.Length];
    }

    // Use this for initialization
    void Start ()
    {
        Shield_L = transform.GetChild(1).gameObject;
        Shield_R = transform.GetChild(2).gameObject;
        Core = transform.GetChild(3).gameObject;
        Fire = transform.GetChild(4).gameObject;

        for (int i = 0; i < pad.Length; i++)
        {
            pad[i] = TriggerObjects[i].GetComponent<PressurePad>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool success = true;

        for (int i = 0; i < pad.Length; i++)
        {
            if (pad[i].IsTrigger == false)
            {
                success = false;
            }
        }

        if (success == true && IsActive == false)
        {
            IsActive = true;
            StartCoroutine(Active());
        }
    }

    IEnumerator Active()
    {
        Fire.SetActive(true);

        for (float i = 0; i < 100.0f; i += 1.0f)
        {
            Shield_L.transform.Translate(Vector3.forward * openSpeed * Time.fixedDeltaTime);
            Shield_R.transform.Translate(Vector3.back * openSpeed * Time.fixedDeltaTime);
            Core.transform.transform.Translate(Vector3.up * openSpeed * 0.2f * Time.fixedDeltaTime);

            yield return 0;
        }
  
        /*
        yield return new WaitForSeconds(2);

        for (float i = 0; i < 100.0f; i += 1.0f)
        {
            Shield_L.transform.Translate(Vector3.back * 0.5f * Time.fixedDeltaTime);
            Shield_R.transform.Translate(Vector3.forward * 0.5f * Time.fixedDeltaTime);
            Core.transform.transform.Translate(Vector3.down * 0.1f * Time.fixedDeltaTime);
            yield return 0;
        }
        */
//        IsActive = false;
    }
}
