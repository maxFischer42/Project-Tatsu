using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeParams : MonoBehaviour
{
    public string objectToDetect;
    public float distance;
    public string boolToEnable;
    public string boolToDisable;

    public RangeParams(string objectToDetect, float distance, string boolEn, string boolDi)
    {
        this.objectToDetect = objectToDetect;
        this.distance = distance;
        boolToEnable = boolEn;
        boolToDisable = boolDi;
    }
}
