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
        print(physics.rigidbody.velocity);
        physics.rigidbody.velocity = new Vector2(value * horizontalModifyer, 0);
    }

    public void moveY(float value)
    {
        print(physics.rigidbody.velocity);
        physics.rigidbody.velocity = new Vector2(0, value * verticalModifyer);
    }
}
