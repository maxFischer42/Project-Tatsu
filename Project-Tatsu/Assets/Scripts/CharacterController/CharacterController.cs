using System.Collections;
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

        private InputController input;

        public bool isGrounded;

        void Start()
        {
            physicsController = GetComponent<PhysicsController>();
            actionController = GetComponent<ActionController>();
            state = CharacterState.State.Idle;
            input = GetComponent<AiInput>().InputController;
        }
        /*
        //TODO create a seperate input class and import it as variable
        //'inputs' to hold more than just movement
        this is controlled by an external character controller that is based off of whether
        this character is being controlled by the player or by an AI
        */
        public Vector2 movementInputs = Vector2.zero;
        public bool actionInput1 = false;

        public void SetInputs()
        {
            movementInputs = input.direction;
            actionInput1 = input.action1;
            print("inputs : " + ("{movement: " + this.movementInputs + "} ") + ("{action1: " + this.actionInput1 + "}"));
        }

        public void Update()
        {
            SetInputs();
            CheckMovement();
            isGrounded = physicsController.DetectGround();
        }

        //checks whether the character's movement is equal to {0,0}
        void CheckMovement()
        {
            if(movementInputs.x != 0)
            {
                actionController.moveX(movementInputs.x);
            }
            if(actionInput1 && isGrounded)
            {
                actionInput1 = false;
                actionController.jump(data.jumpForce);
            }
        }
    }
}