using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementParams : MonoBehaviour
{
    public Vector3 direction;
    public float duration;
    public string boolToEnable;
    public string boolToDisable;

    public MovementParams(Vector3 direction, float duration)
    {
        this.direction = direction;
        this.duration = duration;
    }
}
