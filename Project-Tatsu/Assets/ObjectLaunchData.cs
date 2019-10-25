using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLaunchData : MonoBehaviour
{
    private Rigidbody2D rig;

    public float multiplier = 0.100f;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();    
    }

    public void growKb(int damage, Vector2 velocity)
    {
        float converted = (float)damage / 25;
        multiplier += converted;
        velocity *= multiplier;
        rig.velocity = velocity;
    }
    
}
