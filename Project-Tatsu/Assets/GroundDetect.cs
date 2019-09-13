using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;

public class GroundDetect : MonoBehaviour
{
    private Animator anim;
    public LayerMask layersToDetect;
    public float distance;
    public Transform center;
    private InputController input;
    private Rigidbody2D rig;
    private Controller.CharacterController controller;
    public float vAngle = -0.5f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<Controller.CharacterController>();
        input = GetComponent<InputController>();
        rig = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (checkDir(Vector2.left) || checkDir(Vector2.right))
        {
            StartCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        this.input.action1 = true;
        yield return new WaitForSeconds(Time.deltaTime);
        this.input.action1 = false;
    }

    Vector2 GetDirection()
    {
        Vector2 direction = new Vector2(1,0);
        Vector2 vel = rig.velocity;
        if(vel.x < 0) {
            direction *= -1;
        } else if(vel.x == 0) {
            direction *= 0;
        }
        return direction;
    }

    bool checkDir(Vector2 direction)
    {
        bool value = false;

        //
        LineRenderer r = GetComponent<LineRenderer>();

        Vector2 facingDir = GetDirection();
        Vector3 dir = new Vector2(facingDir.x, vAngle);
        float dis = distance;
        //
        r.SetPosition(0, center.transform.position);

        var hit = Physics2D.Raycast(center.position, dir, dis, layersToDetect);
        //
        

        if (hit.collider == null)
        {
            value = true;
            r.SetPosition(1, center.transform.position + dir);
        }
        else
        {
            r.SetPosition(1, hit.point);
        }
        return value;
    }
}
