using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;
using Controller;

public class MovementHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    private Controller.CharacterController controller;
    private InputController input;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller.CharacterController>();
        input = GetComponent<InputController>();
    }

    public void MovementListener(MovementParams @params)
    {
        var direction = @params.direction;
        this.input.direction = @params.direction;
    }
}
