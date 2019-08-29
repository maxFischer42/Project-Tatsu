using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        setGravity(0);
    }

    public void setGravity(float gravity)
    {
        rigidbody.gravityScale = gravity;
    }

}
