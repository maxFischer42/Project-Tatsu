using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCollision : MonoBehaviour
{
    public bool isCollision = true;
    public bool isTrigger = true;

    private void OnCollisionEnter2D(Collision2D other) {
        if(isCollision)
            print("Collided with " + other.gameObject.name);    
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(isTrigger)
            print("Triggered with " + other.name);    
    }
}
