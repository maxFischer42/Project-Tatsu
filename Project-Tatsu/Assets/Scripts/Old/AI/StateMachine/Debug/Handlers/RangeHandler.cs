using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHandler : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void RangeListener(RangeParams @params)
    {
        GameObject target = GameObject.Find(@params.objectToDetect);
        Vector2 myPosition = gameObject.transform.position;
        Vector2 enPosition = target.transform.position;
        float distance = Vector2.Distance(myPosition, enPosition);
        //Debug.Log(myPosition + " " + enPosition + " " + distance);


        if (@params.distance >= distance)
        {
            anim.SetBool(@params.boolToEnable, true);
            anim.SetBool(@params.boolToDisable, false);
        }

    }
} 