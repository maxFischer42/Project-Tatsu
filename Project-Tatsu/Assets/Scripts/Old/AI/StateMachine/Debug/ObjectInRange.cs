using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInRange : StateMachineBehaviour
{

    public string objectToDetect;
    public float distance;
    public string boolToEnable;
    public string boolToDisable;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var value = new RangeParams(objectToDetect, distance, boolToEnable, boolToDisable);
        animator.gameObject.SendMessage("RangeListener", value);
    }
}
