using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogMenu : MonoBehaviour
{
    public bool isCooling;
    public float cooldown;
    private float c;
    public float rotateSpeed;
    public float timeScale;
    private float direction;
    public int loopAmount;
    private float loop;
    private float startZ;
    public float multiplier = 2;


    public enum menuItems {RED, GREEN, BLUE, YELLOW, none};
    public menuItems currentItem;
    public menuItems pastItem;

    // Update is called once per frame
    void Update()
    {

        if(isCooling) {
            var _d = direction;
            if(loopAmount/multiplier >= (loop) && isCooling) {
                transform.rotation = Quaternion.Euler(new Vector3(0,0,startZ + (loop * direction * multiplier)));
                loop += (multiplier);
            } else {
                loop = 0;
                isCooling = false;
                startZ = startZ + (direction * 90);
                transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.RoundToInt(startZ)));
                SetNewItem();
            }
            return;
        }
        int dirPress = GetDirection();
        if(dirPress != 0) {
            isCooling = true;         
            direction = dirPress;
        }
    }

    void SetNewItem() {
        pastItem = currentItem;
        currentItem = menuItems.none;
        switch(pastItem) {
            case menuItems.BLUE:
                if(direction > 0) {
                    currentItem = menuItems.YELLOW;
                } else {
                    currentItem = menuItems.GREEN;
                }
                break;
            case menuItems.YELLOW:
                if(direction > 0) {
                    currentItem = menuItems.RED;
                } else {
                    currentItem = menuItems.BLUE;
                }
                break;
            case menuItems.RED:
                if(direction > 0) {
                    currentItem = menuItems.GREEN;
                } else {
                    currentItem = menuItems.YELLOW;
                }
                break;
            case menuItems.GREEN:
                if(direction > 0) {
                    currentItem = menuItems.BLUE;
                } else {
                    currentItem = menuItems.RED;
                }
                break;
        }
        pastItem = menuItems.none;
    }

    int GetDirection() {
        var ax = Input.GetAxis("Horizontal");
        if(ax != 0) {
            if(ax > 0)
                return 1;
            else if(ax < 0)
                return -1;
        }
        return 0;
    }
}
