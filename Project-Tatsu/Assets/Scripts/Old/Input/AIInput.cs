using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;
using Inputs;

public class AIInputs : MonoBehaviour
{
    public Controller.CharacterController characterController;
    public Vector2 movement;

    private void Update()
    {
        InputController currentFrameInput = new InputController();
        currentFrameInput = SetDirectionInputs(currentFrameInput);

        characterController.SetInputs();
    }

    InputController SetDirectionInputs(InputController input)
    {
        input.direction = movement;
        return input;
    }
}