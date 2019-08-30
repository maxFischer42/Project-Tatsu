using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;
using Controller;

public class MovementHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    private Controller.CharacterController controller;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller.CharacterController>();
    }

    public void MovementListener(MovementParams @params)
    {
        var direction = @params.direction;
        var input = new InputController();
        input.direction = direction;
        controller.SetInputs(input);
    }
}
