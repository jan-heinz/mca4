using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{ void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            other.transform.SetParent(null);
        }
    }
}