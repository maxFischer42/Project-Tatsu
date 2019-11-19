using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : ActionManager
{
    public PlayerActionList myActions;
    
    void Update()
    {
        
    }

    public void CheckConditions(InputCheck @params)
    {
        bool isCurrentInput = CheckInputAxis(@params.inputAxis);

    }

    public bool CheckInputAxis(string[] inputs)
    {
        for (int i = 0; i < inputs.Length; i++)
        {

        }
    }
        
    
}

[System.Serializable]
public class PlayerActionList
{
    public ActionObject
        punch1,
        punch2
    ;
}
