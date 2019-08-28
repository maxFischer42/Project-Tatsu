using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        void Start()
        {
            physicsController = GetComponent<PhysicsController>();
            actionController = GetComponent<ActionController>();

            physicsController.setGravity(0);
        }

        public void Update()
        {
            //testing relations between the components
            float h = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            if(h != 0f)
            {
                actionController.moveX(h);
            }
            if(y != 0f)
            {
                actionController.moveY(y);
            }
        }
    }
}