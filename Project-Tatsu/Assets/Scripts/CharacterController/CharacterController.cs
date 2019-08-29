﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateData;
using Inputs;

namespace Controller
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PhysicsController))]
    [RequireComponent(typeof(ActionController))]
    public class CharacterController : MonoBehaviour
    {
        public CharacterData data;
        private PhysicsController physicsController;
        private ActionController actionController;
        private CharacterState.State state;

        void Start()
        {
            physicsController = GetComponent<PhysicsController>();
            actionController = GetComponent<ActionController>();
            state = CharacterState.State.Idle;
        }
        /*
        //TODO create a seperate input class and import it as variable
        //'inputs' to hold more than just movement
        this is controlled by an external character controller that is based off of whether
        this character is being controlled by the player or by an AI
        */
        public Vector2 movementInputs = Vector2.zero; 

        public void SetInputs(InputController inputs)
        {
            movementInputs = inputs.direction;
        }

        public void Update()
        {
            CheckMovement();
        }

        //checks whether the character's movement is equal to {0,0}
        void CheckMovement()
        {
            if(movementInputs.x != 0)
            {
                actionController.moveX(movementInputs.x);
            }
            if(movementInputs.y != 0)
            {
                actionController.moveY(movementInputs.y);
            }
        }
    }
}