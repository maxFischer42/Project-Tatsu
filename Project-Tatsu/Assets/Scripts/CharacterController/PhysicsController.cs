using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rigidbody;
    private Controller.CharacterController controller;
    public Transform feetOrigin;
    
    public bool DetectGround()
    {
        bool grounded = false;
        float dis = controller.data.distanceToGround;
        LayerMask mask = controller.data.layersToDetect;
        RaycastHit2D hit = Physics2D.Raycast(feetOrigin.position, -Vector2.up, dis, mask);
        if(hit.collider.tag == "Ground")
        {
            grounded = true;
        }
        return grounded;
    }

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        setGravity(controller.data.gravity);
    }

    public void setGravity(float gravity)
    {
        rigidbody.gravityScale = gravity;
    }
}
