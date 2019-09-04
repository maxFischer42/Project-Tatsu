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
        var hit = Physics2D.Raycast(feetOrigin.position, Vector2.down, dis, mask);
        if (hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "Ground")
            {
                grounded = true;
            }
        }
        return grounded;
    }

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller.CharacterController>();
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        setGravity(controller.data.gravity);
    }

    public void setGravity(float gravity)
    {
        rigidbody.gravityScale = gravity;
    }
}
