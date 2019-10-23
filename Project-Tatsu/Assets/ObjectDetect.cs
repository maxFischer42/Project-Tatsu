using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

[RequireComponent(typeof (Controller.CharacterController))]
public class ObjectDetect : MonoBehaviour
{
    private Controller.CharacterController controller;
    
    private void Start()
    {
        controller = GetComponent<Controller.CharacterController>();
    }


}