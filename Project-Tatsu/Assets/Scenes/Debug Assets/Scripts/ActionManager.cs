using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{

    public bool isCooldown, isAttacking;
    
    public void InstantiateHitbox(AttackObject @params)
    {
        GameObject myHitbox = (GameObject)Instantiate(@params.hitbox, transform);
        myHitbox.transform.localPosition = @params.offset;
        myHitbox.layer = @params.layer;
        Destroy(myHitbox.gameObject, @params.lifetime);
    }
}
