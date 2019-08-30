using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMovement : StateMachineBehaviour
{

    public Vector3 direction;
    public float duration;
    public string boolToEnable;
    public string boolToDisable;

    /* How to call an action, such as movement;
    var value = new MovementParams(Vector3.right, 50f);
    animator.SendMessage("MovementListener", value);
    */

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("On Movement Enter");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("On Movement Exit");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var value = new MovementParams(direction, 50f);
        animator.SendMessage("MovementListener", value);
        Debug.Log("On Movement Update ");
    }

    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("On Movement Move ");
    }

    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("On Movement IK ");
    }
}