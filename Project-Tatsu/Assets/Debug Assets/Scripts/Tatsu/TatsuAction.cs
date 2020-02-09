using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tatsu;

[CreateAssetMenu(fileName = "TatsuAction", menuName = "Tatsu/Action")]
public class TatsuAction : ScriptableObject {

    //_name needs to be unique as it is associated with an animation
    public string _name;

    //_tag is associated with a database managing all possible attacks
    public string _tag;
    public string _group;
    public ActionTypes ActionTypes;
    public GameObject hitbox;
    public float lifeTime = 0.1f;
    public TatsuAction followUp;
    public float followUpTimer;
    public float coolDown = 1.5f;

    public int damage;
}
