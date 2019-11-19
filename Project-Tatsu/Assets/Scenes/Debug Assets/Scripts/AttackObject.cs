using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Assets/Combat/AttackObject")]
public class AttackObject : ScriptableObject
{
    public GameObject hitbox;
    public Vector2 offset;
    public int layer;
    public int damage;
    public float lifetime;
}
