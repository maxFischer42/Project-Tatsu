using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;


public class ControllerAnimator : MonoBehaviour
{
    private CombatController combat;
    private Controller.CharacterController controller;
    private SpriteRenderer sprt;
    public Animator anim;
    public MyAnimations animations;

    // Start is called before the first frame update
    void Start()
    {
        combat = GetComponent<CombatController>();
        controller = GetComponent<Controller.CharacterController>();
        sprt = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody2D>().velocity == Vector2.zero && !controller.actionInput2)
        {
            anim.enabled = false;
            sprt.sprite = animations.idle;
        }
        else
        {
            anim.enabled = true;
        }
        if(controller.actionInput2 && controller.isGrounded)
        {
            CombatController.inputState inputState = combat.getCurrentState();
            switch(inputState)
            {
                case CombatController.inputState.neutral:
                    anim.runtimeAnimatorController = animations.jab;
                    break;
                case CombatController.inputState.ftilt:
                    anim.runtimeAnimatorController = animations.ftilt;
                    break;
                case CombatController.inputState.dtilt:
                    anim.runtimeAnimatorController = animations.dtilt;
                    break;
                case CombatController.inputState.utilt:
                    anim.runtimeAnimatorController = animations.utilt;
                    break;
            }
            resetAnim();
            enabled = false;
        } else if(GetComponent<Rigidbody2D>().velocity.x != 0f)
        {
            anim.runtimeAnimatorController = animations.walk;
        }
        if (!controller.isGrounded)
        {
            if (controller.actionInput2)
            {
                anim.enabled = true;
                CombatController.inputState inputState = combat.getCurrentState();
                switch (inputState)
                {
                    case CombatController.inputState.nair:
                        anim.runtimeAnimatorController = animations.nair;
                        break;
                    case CombatController.inputState.bair:
                        anim.runtimeAnimatorController = animations.bair;
                        break;
                    case CombatController.inputState.fair:
                        anim.runtimeAnimatorController = animations.fair;
                        break;
                    case CombatController.inputState.uair:
                        anim.runtimeAnimatorController = animations.uair;
                        break;
                    case CombatController.inputState.dair:
                        anim.runtimeAnimatorController = animations.dair;
                        break;
                }
                resetAnim();
                enabled = false;
            } else
            {
                anim.enabled = false;
                sprt.sprite = animations.jump;
            }
        }

        
    }

    public void resetAnim()
    {
        anim.StopPlayback();
        anim.Play(anim.runtimeAnimatorController.name);       
    }
}

[System.Serializable]
public class MyAnimations
{
    public RuntimeAnimatorController
        walk,
        jab,
        ftilt,
        utilt,
        dtilt,
        nair,
        fair,
        uair,
        dair,
        bair;
    public Sprite
        idle,
        jump;
}
