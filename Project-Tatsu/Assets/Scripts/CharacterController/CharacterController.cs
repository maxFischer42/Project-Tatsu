using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterController
{
    public class CharacterController : MonoBehaviour
    {
        public CharacterData data;
        void Start()
        {
            print(data.characterName + ", " + data.movement);
        }
    }
}