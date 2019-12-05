using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public Vector2 damageRange = new Vector2(1, 3);
    public float pushForce = 2f;
    public float verticalPush = 2.5f;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<ObjectDamage>())
        {
            print("Detected Object " + collision.gameObject.name);
            int myDamage = Mathf.RoundToInt(Random.Range(damageRange.x, damageRange.y));
            collision.GetComponent<ObjectDamage>().takeDamage(myDamage);
            ApplyForce(collision);
        }
    }

    void ApplyForce(Collider2D collision)
    {
        Transform collider = collision.GetComponent<Transform>();
        Vector2 vPush = new Vector2(0, verticalPush);
        transform.parent.GetComponent<Rigidbody2D>().AddForce(vPush);
        collision.GetComponent<Rigidbody2D>().AddForce(vPush);
        Vector2 direction = collider.position - transform.position;
        direction.Normalize();
        direction *= pushForce;
        direction = new Vector2(direction.x, 0);
        transform.parent.GetComponent<Rigidbody2D>().AddForce(direction);
        collision.GetComponent<Rigidbody2D>().AddForce(direction);
    }
}
