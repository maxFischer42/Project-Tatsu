using UnityEngine;
using Attack;

[CreateAssetMenu]
public class AttackData : ScriptableObject
{
    public AttackBox.hitboxShape shape;

    public float x;
    public float y;

    //used for the circle and capsule hitboxes only
    public float h;
    public float k;

    //offset for the hitbox
    public float xOff;
    public float yOff;

    public float duration; // duration in frames
    public float coolDown; // duration in frames after the action until another input can be enterd

    public int damage;

    public Vector2 knockback; // knockback multiplier, x = base knockback at full hp, y = knockback at <= 1 hp 
    public int launchDir; //the id of what kind of knockback is applied to the damage,

    public int effect; //the id of the effect that plays when an attack connects
    public float effectMultiplier; //the intensity of the effect that is played
}
