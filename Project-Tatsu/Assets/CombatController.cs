﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;
using Inputs;
using Attack;
namespace Combat {
    public class CombatController : MonoBehaviour
    {
        private Controller.CharacterController controller;
        public AttackList attacks;
        public AttackBox hitboxHolder;
        public InputController input;

        public enum inputState { neutral, ftilt, dtilt, utilt, nair, fair, dair, bair, uair, grab, error }
        public inputState currentState;

        void Start()
        {
            controller = GetComponent<Controller.CharacterController>();
            input = GetComponent<InputController>();
        }
 
        void Update()
        {       
            if(input.action2 && !hitboxHolder.doingAttack)
            {
                AttackData d = attacks.neutral;
                inputState myState = getCurrentState();
                switch(myState)
                {
                    case inputState.neutral:
                        d = attacks.neutral;
                        break;
                    case inputState.ftilt:
                        d = attacks.fTilt;
                        break;
                    case inputState.dtilt:
                        d = attacks.dTilt;
                        break;
                    case inputState.utilt:
                        d = attacks.uTilt;
                        break;
                    case inputState.uair:
                        d = attacks.uAir;
                        break;
                    case inputState.fair:
                        d = attacks.fAir;
                        break;
                    case inputState.bair:
                        d = attacks.bAir;
                        break;
                    case inputState.dair:
                        d = attacks.dAir;
                        break;
                    case inputState.nair:
                        d = attacks.nAir;
                        break;
                }
                createAttack(d);
            }
        }

        void createAttack(AttackData data)
        {
            hitboxHolder.action(data);
            print(getCurrentState());
        }

        public Vector2 getFlip() {
            if(!GetComponent<SpriteRenderer>().flipX)
            {
                return Vector2.right;       
            }
            return Vector2.left;
        }

        //listeners for creating or destroying a hitbox
        public void cBox(AttackData d)
        {
            hitboxHolder.createBox(d);
        }
        public void dBox()
        {
            hitboxHolder.destroyBox();
        }


        public inputState getCurrentState()
        {
            //TODO this will probably end up pretty spaghetti so refactor later
            bool grounded = controller.isGrounded;
            Vector2 facingDir = getFlip();
            Vector2 currentDir = input.direction;

            if(grounded) //do grounded
            {
                if(currentDir == Vector2.zero)
                {
                    return inputState.neutral; //jab attack
                } else if (currentDir.x != 0f && currentDir.y == 0f)
                {
                    return inputState.ftilt; //forward tilt attack
                } else if (currentDir.y != 0f)
                {
                    //check for u/d tilt
                    if(currentDir.y > 0f)
                    {
                        return inputState.utilt;
                    }
                    else
                    {
                        return inputState.dtilt;
                    }
                }
            
            } else //do aerials 
            {
                Vector2 normalDir = currentDir.normalized;
                if (currentDir == Vector2.zero)
                {
                    return inputState.nair;
                }
                else if (normalDir.x == facingDir.x)
                {
                    return inputState.fair;
                } else if (normalDir.x == facingDir.x * -1)
                {
                    return inputState.bair;
                }
                else if(currentDir.y != 0)
                {
                    if(currentDir.y > 0)
                    {
                        return inputState.uair;
                    } else if(currentDir.y < 0)
                    {
                        return inputState.dair;
                    }
                }
            }
            Debug.LogError("No Input for current attack");
            return inputState.error;
        }
    }
}

[System.Serializable]
public class AttackList {
    public AttackData
        neutral,
        fTilt,
        dTilt,
        uTilt,
        nAir,
        fAir,
        bAir,
        dAir,
        uAir;
}