using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;

public class AiInput : MonoBehaviour
{
    public InputController InputController;

    private void Start()
    {
        InputController = new InputController();
    }
}
