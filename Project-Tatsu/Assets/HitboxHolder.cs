using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxHolder : MonoBehaviour
{
    public AttackData data;
    public float flip = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            return;
        }
        print(collision.gameObject.name);
        Vector2 kb = data.knockback;
        Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();
        float angle = data.launchDir;
        Vector2 velocity = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
        velocity *= kb;
        velocity = new Vector2(velocity.x * flip, velocity.y);
        rig.velocity = velocity;
    }
}
