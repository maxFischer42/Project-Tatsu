using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamage : MonoBehaviour
{

    public int maxHP;
    public int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void takeDamage(int damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

}
