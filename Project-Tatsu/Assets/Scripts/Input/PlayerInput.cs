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

        characterController.SetInputs(currentFrameInput);
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
