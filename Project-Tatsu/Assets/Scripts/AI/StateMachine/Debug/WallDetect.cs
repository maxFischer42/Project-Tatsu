using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class WallDetect : MonoBehaviour
{
    private Animator anim;
    public LayerMask layersToDetect;
    public float distance;
    public Transform center;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if(checkDir(Vector2.left) || checkDir(Vector2.right))
        {
            StartCoroutine(Jump());
        }      
    }

    IEnumerator Jump()
    {      
        GetComponent<Controller.CharacterController>().actionInput1 = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Controller.CharacterController>().actionInput1 = false;        
    }

    bool checkDir(Vector2 direction)
    {
        bool value = false;
        Vector2 dir = direction;
        float dis = distance;
        var hit = Physics2D.Raycast(center.position, dir, dis, layersToDetect);
        if (hit.collider != null)
        {
            print("foo");
            value = true;
        }
        return value;
    }
}
