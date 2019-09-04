using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName = "null";
    public float movement = 0f;
    public float gravity = 1f;
    public float distanceToGround = 0.1f;
    public LayerMask layersToDetect;
}