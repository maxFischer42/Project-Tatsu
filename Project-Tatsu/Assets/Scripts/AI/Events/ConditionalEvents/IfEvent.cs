using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Events/Conditional/If")]
public class IfEvent : AiEvent
{
    public AiEvent condition;
    public string infoRequest;
    public AiEvent action;
    public AiEvent elseAction;

    public void Play()
    {
        if (condition.GetInfo(infoRequest))
        {
            action.Play();
        }
        else
        {
            elseAction.Play(); //in the case an "else if" needs to be used, elseAction must be a conditional event
        }

    }
}
