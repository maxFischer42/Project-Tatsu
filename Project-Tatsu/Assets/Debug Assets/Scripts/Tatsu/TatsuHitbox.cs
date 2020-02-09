using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tatsu;

public class TatsuHitbox : MonoBehaviour {
    public string _tag;
    public ActionTypes types;
    public TatsuAction _action;
    public void setTag(string _tag, ActionTypes _type) {
        this._tag = _tag;
        this.types = _type;
    }

    public void OnTriggerEnter2D(Collider2D other) {
//        print(other.name);
    }
}