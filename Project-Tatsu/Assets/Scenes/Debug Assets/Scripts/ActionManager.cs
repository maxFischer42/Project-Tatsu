using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public bool isCooldown;
    public bool isAttacking;
    private ActionObject previousAction;
    private Animator animator;
    public List<Behaviour> togglableMechanics = new List<Behaviour>();
    public ActionObject prevAction;
    public bool followUpChance;
    public float followUp = 0.5f;

    public IEnumerator Cooldown()
    {
        print("Starting Cooldown...");
        yield return new WaitForSeconds(0.2f);
        isCooldown = false;
        isAttacking = false;
        ToggleMechanics(true);
        animator.SetBool("endAttack", true);
        yield return new WaitForSeconds(0.06f);
        animator.SetBool("endAttack", false);
    }

    public IEnumerator followUpChanceTimer()
    {
        followUpChance = true;
        yield return new WaitForSeconds(followUp);
        followUpChance = false;
    }

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void InstantiateHitbox(AttackObject @params)
    {
        GameObject myHitbox = (GameObject)Instantiate(@params.hitbox, transform);
        myHitbox.transform.localPosition = @params.offset;
        myHitbox.layer = @params.layer;
        Destroy(myHitbox.gameObject, @params.lifetime);
    }

    public bool CheckPreviousAction(ActionObject action)
    {
        bool value = false;
        if (action == previousAction)
        {
            value = true;
        }
        return value;
    }

    public bool VerifyConditions(List<bool> conditionList, bool state)
    {
        bool[] conditions = conditionList.ToArray();
        for (int i = 0; i < conditions.Length; i++)
        {
            if (conditions[i] == !state)
            {
                return false;
            }
        }
        return true;
    }

    public void ToggleMechanics(bool state) {
        Behaviour[] behaviours = togglableMechanics.ToArray();
        for (int i = 0; i < behaviours.Length; i++)
        {
            //behaviours[i].enabled = state;
        }       
    }

    public void PerformAction(string triggerName)
    {
        if (isAttacking || isCooldown)
            return;        
        isAttacking = true;
        isCooldown = true;
        animator.SetBool(triggerName, true);
        ToggleMechanics(false);
        StartCoroutine(delayTrigger(triggerName));
    }

    IEnumerator delayTrigger(string triggerName)
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool(triggerName, false);
    }

    public void HandleAction(AttackObject @params)
    {
        GameObject hitbox = (GameObject)Instantiate(@params.hitbox, transform);
        Vector2 offset = @params.offset;
        if(checkFlip())
        {
            offset = new Vector2(offset.x * -1, offset.y);
        }
        hitbox.transform.localPosition = offset;
        hitbox.layer = @params.layer;
        Destroy(hitbox.gameObject, @params.lifetime);
    }

    public bool checkFlip()
    {
        bool flip = GetComponent<SpriteRenderer>().flipX;
        return flip;
    }
    
    public void HandleEnd()
    {
        //isAttacking = false;
        StartCoroutine(Cooldown());
        StartCoroutine(followUpChanceTimer());
    }
}   