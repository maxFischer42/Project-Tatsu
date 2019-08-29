using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class ActionController : MonoBehaviour
{
    public float horizontalModifyer = 1f;
    public float verticalModifyer = 1f;
    private PhysicsController physics;

    void Start()
    {
        physics = gameObject.GetComponent<PhysicsController>();
    }

    public void moveX(float value)
    {
        physics.rigidbody.velocity = new Vector2(value * horizontalModifyer, physics.rigidbody.velocity.y);
    }

    public void moveY(float value)
    {
        physics.rigidbody.velocity = new Vector2(physics.rigidbody.velocity.x, value * verticalModifyer);
    }
}
