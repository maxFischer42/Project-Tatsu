using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;
using Inputs;
using Combat;

public class PlayerInput : MonoBehaviour
{
    public Controller.CharacterController characterController;
    public CombatController combatController;

    private void Update()
    {
        InputController currentFrameInput = new InputController();
        currentFrameInput = SetDirectionInputs(currentFrameInput);
        currentFrameInput = SetActionInputs(currentFrameInput);

        combatController.input = currentFrameInput;
        characterController.input = currentFrameInput;
    }

    InputController SetActionInputs(InputController input)
    {
        bool action1 = Input.GetButtonDown("Action1");
        bool action2 = Input.GetButtonDown("Action2");
        input.action1 = action1;
        input.action2 = action2;
        return input;
    }

    InputController SetDirectionInputs(InputController input)
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(x,y);
        input.direction = direction;
        return input;
    }


}
