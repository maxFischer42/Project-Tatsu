using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Attack {
    public class AttackBox : MonoBehaviour
    {
        public enum hitboxShape { circle, box, capsule }
        public GameObject hitboxPrefab;
        public bool doingAttack = false;
        AttackData dataObj;
        public GameObject hitBoxObject;
        public GameObject currentBox;
        public GameObject effectObj;

        hitboxShape shape;
        float x;
        float y;
        float h;
        float k;
        float xOff;
        float yOff;
        float duration;
        float coolDown;
        int damage;    
        Vector2 knockback;
        int launchDir;
        int effect;
        float effectMultiplier;

        public AttackBox(AttackData data)
        {
            dataObj = data;
        }

        public void action(AttackData data)
        {
            dataObj = data;
            Begin();
        }

        public void Begin()
        {
            setTransform();
        }

        void setTransform()
        {
            this.x = dataObj.x;
            if(GetComponentInParent<Combat.CombatController>().getFlip() == Vector2.left)
            {
                this.x *= -1;
            }
            this.y = dataObj.y;
            this.xOff = dataObj.xOff;
            this.yOff = dataObj.yOff;
            if (dataObj.shape != hitboxShape.box)
            {
                this.h = dataObj.h;
                this.k = dataObj.k;
            }
        }

        void setCollider()
        {
            Vector2 offset = new Vector2(xOff, yOff);
            Vector2 size = size = new Vector2(x, y);
            hitBoxObject.GetComponent<CircleCollider2D>().enabled = false;
            hitBoxObject.GetComponent<BoxCollider2D>().enabled = false;
            hitBoxObject.GetComponent<CapsuleCollider2D>().enabled = false;

            switch (shape)
            {
                case hitboxShape.circle:
                    hitBoxObject.GetComponent<CircleCollider2D>().enabled = true;
                    hitBoxObject.GetComponent<CircleCollider2D>().radius = h;
                    hitBoxObject.GetComponent<CircleCollider2D>().offset = offset;
                    break;
                case hitboxShape.box:
                    hitBoxObject.GetComponent<BoxCollider2D>().enabled = true;
                    hitBoxObject.GetComponent<BoxCollider2D>().offset = offset;
                    hitBoxObject.GetComponent<BoxCollider2D>().size = size;
                    break;
                case hitboxShape.capsule:
                    hitBoxObject.GetComponent<CapsuleCollider2D>().enabled = true;
                    hitBoxObject.GetComponent<CapsuleCollider2D>().offset = offset;
                    hitBoxObject.GetComponent<CapsuleCollider2D>().size = size;
                    break;
            }
        }

        public void createBox(AttackData d)
        {
            doingAttack = true;
            float flip = 1;
            if(transform.parent.GetComponent<SpriteRenderer>().flipX)
            {
                flip = -1;
            }
            hitBoxObject = (GameObject)Instantiate(hitboxPrefab, transform);
            hitBoxObject.transform.localScale = new Vector2(x, y);
            hitBoxObject.GetComponent<HitboxHolder>().flip = flip;
            hitBoxObject.GetComponent<HitboxHolder>().data = d;
            currentBox = hitBoxObject;
            setCollider();           
        }

        public void destroyBox()
        {
            Destroy(currentBox.gameObject);
            GetComponentInParent<ControllerAnimator>().enabled = true;
            doingAttack = false;
            if (effectObj.gameObject)
                Destroy(effectObj.gameObject);
        }

        void setHit()
        {
            this.damage = dataObj.damage;
            this.knockback = dataObj.knockback;
            this.launchDir = dataObj.launchDir;
            this.effect = dataObj.effect;
            this.effectMultiplier = dataObj.effectMultiplier;
        }

    }
}