using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Assets/Hurtboxes")]
public class HurtBoxObject : ScriptableObject
{
    public Vector2 offset = Vector2.zero;
    public HurtboxPoints points;
    public bool lastFrame = false;
    
}

public class HurtBoxEditor : Editor
{
   
}
