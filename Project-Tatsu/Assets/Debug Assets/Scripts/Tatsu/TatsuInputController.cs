using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Tatsu {

    [RequireComponent(typeof(TatsuCombatController))]
    public class TatsuInputController : MonoBehaviour {

        public bool isPlayer = false;
        private InputManager inputManager;
        public DemoScene playerDemoscene;

        Inputs definitions = new Inputs();

        private void Start() {
            gameObject.AddComponent<InputManager>();
            inputManager = GetComponent<InputManager>();
            if(isPlayer) {
                playerDemoscene = GetComponent<DemoScene>();
            }    
        }

        public void InputListener(InputData _data) {
            var _axis = _data.name;
            var _value = _data.value;
            var data = definitions.data;
            //get the currently hit input and sets it to true
            if(isPlayer)
                SetPlayerInput(_axis, _value);                  
        }

        void SetPlayerInput(string name,float _value) {
            //print("setting player input "+ name + ":" + _value);
            #region horizontal
            if(name == definitions.horizontal) {
                if(_value < 0) {
                    playerDemoscene.right = true;
                    playerDemoscene.left = false;
                } else if (_value > 0) {
                    playerDemoscene.right = false;
                    playerDemoscene.left = true;
                } else {
                    playerDemoscene.right = false;
                    playerDemoscene.left = false;
                }
            }
            #endregion

            #region vertical
            if(name == definitions.vertical) {
                if(_value < 0) {
                    playerDemoscene.down = true;
                    playerDemoscene.up = false;
                } else if (_value > 0) {
                    playerDemoscene.down = false;
                    playerDemoscene.up = true;
                } else {
                    playerDemoscene.down = false;
                    playerDemoscene.up = false;
                }
            }
            #endregion

            #region actions
            if(name == definitions.actionOne) {
                if(_value > 0)
                    playerDemoscene.doActionOne();
            } else if(name == definitions.actionTwo) {
                if(_value > 0)
                    playerDemoscene.doActionTwo();
            } else if(name == definitions.actionThree) {
                playerDemoscene.aOne = false;
                playerDemoscene.aTwo = false;
                playerDemoscene.aThree = true;
            } else {
                playerDemoscene.aOne = false;
                playerDemoscene.aTwo = false;
                playerDemoscene.aThree = false;
            }
            #endregion

            #region jump
            if(name == definitions.jump && _value > 0) {
                playerDemoscene.jump = true;
            } else {
                playerDemoscene.jump = false;
            }
            #endregion
        }

    }

    [System.Serializable]
    public class InputManager : MonoBehaviour{

        public string horizontalAxis = "Horizontal";
        public string verticalAxis = "Vertical";
        public string actionOne = "Action1";
        public string actionTwo = "Action2";
        public string actionThree = "Action3";
        public string jump = "Jump";

        private void Update() {
            if(Input.GetAxisRaw(horizontalAxis) != 0) {
                var value = Input.GetAxisRaw(horizontalAxis);
                InputData data = new InputData(horizontalAxis, value);
                SendMessage("InputListener", data);
            } else {
                var value = 0;
                InputData data = new InputData(horizontalAxis, value);
                SendMessage("InputListener", data);
            }

            if(Input.GetAxisRaw(verticalAxis) != 0) {
                var value = Input.GetAxisRaw(verticalAxis);
                InputData data = new InputData(verticalAxis, value);
                SendMessage("InputListener", data);
            } else {
                var value = 0;
                InputData data = new InputData(verticalAxis, value);
                SendMessage("InputListener", data);
            }

            if(Input.GetButtonDown(actionOne)) {
                InputData data = new InputData(actionOne, 1);
                SendMessage("InputListener", data);
            } else {
                InputData data = new InputData(actionOne, 0);
                SendMessage("InputListener", data);
            }
            
            if(Input.GetButtonDown(actionTwo)) {
                InputData data = new InputData(actionTwo, 1);
                SendMessage("InputListener", data);
            } else {
                InputData data = new InputData(actionTwo, 0);
                SendMessage("InputListener", data);
            }

            if(Input.GetAxisRaw(actionThree) != 0) {
                var value = Input.GetAxisRaw(actionThree);
                InputData data = new InputData(actionThree, value);
                SendMessage("InputListener", data);
            }  else {
                var value = 0;
                InputData data = new InputData(actionThree, value);
                SendMessage("InputListener", data);
            }

            if(Input.GetButtonDown(jump)) {
                InputData data = new InputData(jump, 1);
                SendMessage("InputListener", data);
            } else {
                var value = 0;
                InputData data = new InputData(jump, value);
                SendMessage("InputListener", data);
            }
        }

    }

    public class InputData {
        public string name;
        public float value;

        public InputData(string name, float value) {
            this.name = name;
            this.value = value;

        }
    }

    public class Inputs {
        public string horizontal = "Horizontal";
        public string vertical = "Vertical";
        public string actionOne = "Action1";
        public string actionTwo = "Action2";
        public string actionThree = "Action3";
        public string jump = "Jump";

        public string[] data = new string[6];

        private void Start() {
            data = new string[6];
            data[0] = horizontal;
            data[1] = vertical;
            data[2] = actionOne;
            data[3] = actionTwo;
            data[4] = actionThree;
            data[5] = jump;
        }
    }
}