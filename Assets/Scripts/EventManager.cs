using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ModDelegate();
public class EventManager : MonoBehaviour {

    public static EventManager Instance;

    // Use this for initialization
    void Start () {
        Instance = this;
	}


	// Update is called once per frame
	void Update () {
		
	}
}
