using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : ActionManager
{
    public PlayerActionList myActions;
    
    private void Awake()
    {
        togglableMechanics.Add(this.GetComponent<MainController>());
        togglableMechanics.Add(this.GetComponent<Grappling>());
    }

    private void Update()
    {
        CheckInputs();
    }

    void CheckInputs()
    {
        for(int i = 0; i < myActions.actions.Length; i++)
        {
            if(CheckConditions(myActions.actions[i].actionInputs))
            {
                PerformAction(myActions.actions[i].boolTrigger);
                return;
            }
        }
    }

    public bool CheckConditions(InputCheck @params)
    {
        if(isAttacking || isCooldown || GetComponent<Grappling>().ropeAttached)
        {
            return false;
        }
        List<bool> conditionData = new List<bool>();
        bool isCurrentInput = CheckInputAxis(@params.inputAxis);
        conditionData.Add(isCurrentInput);
        
        if(@params.isFollower)
        {
            bool previous = CheckPreviousAction(@params.followingAttack);
            conditionData.Add(previous);
        }
        bool verifyInput = VerifyConditions(conditionData,true);
        return verifyInput;
    }

    public bool CheckInputAxis(string[] data)
    {
        bool value = false;
        for (int i = 0; i < data.Length; i++)
        {
            if(Input.GetButtonDown(data[i])) {
                value = true;
            } else
            {
                return false;
            }
        }
        return value;
    }     
    
}

[System.Serializable]
public class PlayerActionList
{
    public ActionObject[] actions;
}
