using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;
using Inputs;

public class PlayerInput : MonoBehaviour
{
    public Controller.CharacterController characterController;

    private void Update()
    {
        InputController currentFrameInput = new InputController();
        currentFrameInput = SetDirectionInputs(currentFrameInput);
        currentFrameInput = SetActionInputs(currentFrameInput);

        characterController.SetInputs();
    }

    InputController SetActionInputs(InputController input)
    {
        bool action1 = Input.GetButtonDown("Action1");
        input.action1 = action1;
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
