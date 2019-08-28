using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class PhysicsController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void setGravity(float gravity)
    {
        rigidbody.gravityScale = gravity;
    }


}
