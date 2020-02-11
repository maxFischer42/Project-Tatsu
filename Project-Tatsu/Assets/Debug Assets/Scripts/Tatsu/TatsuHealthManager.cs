using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

namespace Tatsu{

    public class TatsuHealthManager : MonoBehaviour
    {

        public string[] vulnerableGroups;
        
        public int maxHP = 10;
        private int hp;
        public float knockbackMultiplier = 3f;

        private void Start() {
            hp = maxHP;    
        }

        void TakeDamage(int _damage) {
            hp -= _damage;
            if(hp <= 0) {
                Kill();
            }
        }

        void HandleKnockback(TatsuAction _action, GameObject _parent) {
            float multiplierIndex = maxHP - hp;
            float multiplier = _action.ActionTypes._launchMultiplierCurve.Evaluate(multiplierIndex);
            Vector2 direction = _parent.GetComponent<DemoScene>().faceDirection;
            Vector2 launchDirection = _action.ActionTypes.launchDirection;
            launchDirection = new Vector2(launchDirection.x * direction.x, launchDirection.y);

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().velocity = launchDirection * knockbackMultiplier;
           _parent.GetComponent<CharacterController2D>().move(launchDirection);
                                              
        }

        void Kill() {
            Destroy(gameObject);
        }

        bool checkGroup(string _groupID) {
            for (int i = 0; i < vulnerableGroups.Length; i++) {
                if(vulnerableGroups[i] == _groupID) {
                    return true;
                }
            }
            return false;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.GetComponent<TatsuHitbox>()) {
            //print("connected attack");
                var action = other.GetComponent<TatsuHitbox>()._action;
                bool isWeak = checkGroup(action._group);
                if(isWeak) {
                    print("is weak, " + action.damage);
                    TakeDamage(action.damage);
                    HandleKnockback(action, other.transform.parent.gameObject);
                }
            }
        }

        /*private void OnTriggerStay2D(Collider2D other) {
            print("connected attack");
            if(other.GetComponent<TatsuHitbox>()) {
                var action = other.GetComponent<TatsuHitbox>()._action;
                bool isWeak = checkGroup(action._group);
                if(isWeak) {
                    TakeDamage(action.damage);
                }
            }
        }*/
    }
}