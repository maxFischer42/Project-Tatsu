using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attack;

public class HitboxHolder : MonoBehaviour
{
    public AttackData data;
    public float flip = 1f;
    public GameObject effectObj;

    public GameObject GetEffect(int i)
    {
        print("Effects/" + i + ".prefab");
        var a = Resources.Load("Effects/" + i) as GameObject;
        Debug.Log(a);
        return a;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || !collision.GetComponent<ObjectLaunchData>())
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
        GameObject e = GetEffect(data.effect);
        effectObj = (GameObject)Instantiate(e, collision.gameObject.transform);
        effectObj.gameObject.transform.localScale *= data.effectMultiplier;
        transform.GetComponentInParent<AttackBox>().effectObj = effectObj;
        collision.GetComponent<ObjectLaunchData>().growKb(data.damage,velocity);
    }
}
