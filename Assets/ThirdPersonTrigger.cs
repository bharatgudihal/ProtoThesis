using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SendMessageUpwards("ThirdPersonTriggerEnter");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            SendMessageUpwards("ThirdPersonTriggerStay");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SendMessageUpwards("ThirdPersonTriggerExit");
        }
    }
}
