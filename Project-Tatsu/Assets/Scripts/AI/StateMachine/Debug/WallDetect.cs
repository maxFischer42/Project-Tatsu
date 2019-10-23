using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;
using Inputs;

public class WallDetect : MonoBehaviour
{
    private Animator anim;
    public LayerMask layersToDetect;
    public float distance;
    public Transform center;
    private InputController input;

    private Controller.CharacterController controller;

    private void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<Controller.CharacterController>();
        input = GetComponent<InputController>();
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
        print("theres a wall, idiot");
        this.input.action1 = true;
        yield return new WaitForSeconds(Time.deltaTime);
        this.input.action1 = false;
    }


    bool checkDir(Vector2 direction)
    {
        bool value = false;
        Vector2 dir = direction;
        float dis = distance;
        var hit = Physics2D.Raycast(center.position, dir, dis, layersToDetect);
        if (hit.collider != null)
        {            
            value = true;
        }
        return value;
    }
}
