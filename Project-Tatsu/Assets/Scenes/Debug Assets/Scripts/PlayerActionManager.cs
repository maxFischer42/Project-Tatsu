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
        for (int i = 0; i < myActions.actions.Length; i++)
        {
            if (CheckConditions(myActions.actions[i].actionInputs, myActions.actions[i]))
            {
                prevAction = myActions.actions[i];
                PerformAction(myActions.actions[i].boolTrigger);
                return;
            }
        }
    }

    public bool CheckConditions(InputCheck @params, ActionObject @action)
    {
        if (isAttacking || isCooldown || GetComponent<Grappling>().ropeAttached)
        {
            return false;
        }
        if(action == prevAction)
        {
            return false;
        }
        if (@params.isFollower && (prevAction == @params.followingAttack) && followUpChance && Input.GetButtonDown(@params.inputAxis[0]))
        {
            print("FOLLOW UP");
            return true;
        }
        List<bool> conditionData = new List<bool>();
        bool isCurrentInput = CheckInputAxis(@params.inputAxis);
        conditionData.Add(isCurrentInput);
                
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
