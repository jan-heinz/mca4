using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushProp : MonoBehaviour {

    public float pushForce = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Projectile")) {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * pushForce, ForceMode.Impulse);
        }
    }
}
