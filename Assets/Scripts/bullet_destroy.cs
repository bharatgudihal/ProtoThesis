using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_destroy : MonoBehaviour
{
	public float destroy_time = 1.0f;

    // Use this for initialization
    void OnEnable ()
    {
        StartCoroutine(WaitToDestroy());
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(destroy_time);
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {

	}
}
