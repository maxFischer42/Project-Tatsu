using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Prime31;

namespace Tatsu {

//////////////////////////////////////////////////////////////////////////////
// TatsuCombatController /////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////
// Used by a TatsuController object to perform combat related actions ////////
//////////////////////////////////////////////////////////////////////////////

    [RequireComponent(typeof(TatsuInputController))]

    public class TatsuCombatController : MonoBehaviour
    {
        public CombatSettings settings;    
        public DemoScene playerDemo;
        public CharacterController2D controller2D;

        public TatsuWeapon myWeapon;

        private TatsuAction previousAction;
        private float actionCountdown;

        public LayerMask enemyLayers;

        public bool isAction = false;

        public bool onCooldown = false;
        private float _cooldown = 0f;

        public float grappleLaunchMultiplier = 2f;
        
        public void Action(TatsuAction _action) {

            var newAction = _action;
            if(isAction || onCooldown)
                return;
//            print("Performing Action " + newAction._name );
            isAction = true;
            string myName = newAction._name;
            string myTag = newAction._tag;

            GameObject hitBoxObject = (GameObject)Instantiate(newAction.hitbox, transform);
            Destroy(hitBoxObject, newAction.lifeTime);

            hitBoxObject.name = myTag;
            hitBoxObject.GetComponent<TatsuHitbox>().setTag(myTag, newAction.ActionTypes);
            previousAction = newAction;
            playerDemo.PlayAnimation(myName);
//            print("action");
            actionCountdown = newAction.followUpTimer;
            onCooldown = true;
            _cooldown = newAction.coolDown;
        }        

        private void Update() {
            if(onCooldown) {
                _cooldown -= Time.deltaTime;
                if(_cooldown <= 0) {
                    _cooldown = 0f;
                    onCooldown = false;
                    isAction = false;
                }        
            }
            if(isGrappling)
                return;
            
            if(actionCountdown > 0){
                actionCountdown -= Time.deltaTime;
            }    
            if(actionCountdown <= 0) {
                playerDemo.prevAction = null;
                actionCountdown = 0f;
            }
        }


        public void doGrappleLaunch() {
            Vector2 dir = new Vector2(playerDemo.faceDirection.x, 4);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir,  999f, enemyLayers);
            if(hit) {
                print("grapple");            
                isGrappling = true;
                grappleDirection = dir;
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            isGrappling = false;
            _grappleTimer = 0f;            
        }

        public bool isGrappling = false;
        public float grappleDuration;
        private float _grappleTimer;
        private Vector3 grappleDirection;

        void FixedUpdate() {

            if(isGrappling) {
                _grappleTimer += Time.deltaTime;
                controller2D.move(grappleDirection.normalized * grappleLaunchMultiplier);
                if(_grappleTimer >= grappleDuration) {
                    isGrappling = false;
                    _grappleTimer = 0f;
                }
            }
        }
    }

    [System.Serializable]
    public class CombatSettings {
        public bool isAI;

    } 




//////////////////////////////////////////////////////////////////////////////
//  ActionTypes //////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////
// Used for setting specific settings for an action used by a controller /////
//////////////////////////////////////////////////////////////////////////////

    [System.Serializable]
    public class ActionTypes {
        
        //movement types: Static = no movement, horizontal = horizontal only movement,
        // vertical = vertical only movement, dynamic = x and y, custom = imported
        public enum Movement { Static, Horizontal, Vertical, Dynamic, Custom};
        public Movement _movementType;
        public MovementDefinition _movementDefinition;

        //(1,0,0) facing right, will flip x if facing left
        public Vector2 launchDirection;

        public AnimationCurve _launchMultiplierCurve;



    }

    [System.Serializable]
    public class MovementDefinition {
        public float _xMovement, _yMovement, _xMultiplier, _yMultiplier;
    }



}
