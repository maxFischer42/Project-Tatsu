using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowBob : MonoBehaviour
{

    public CogMenu cog;
    private Image image;
    private Text t;
    public CogMenu.menuItems items;
    public bool isTitle;

    void Start() {
        if(!isTitle) {
            image = GetComponent<Image>();
        }
        else {
            t = GetComponent<Text>();
        }
    }

    void Update() {
        if(cog.isCooling) {
            if(!isTitle)
                image.enabled = false;
            else
            {
                t.enabled = false;
                t.text = "";
            }
        } else {
            if(isTitle) {
                t.enabled = true;
                switch(cog.currentItem) {
                    case CogMenu.menuItems.BLUE:
                        t.text = "Inventory";
                        break;
                    case CogMenu.menuItems.GREEN:
                        t.text = "Quests";
                        break;
                    case CogMenu.menuItems.RED:
                        t.text = "Equipment";
                        break;
                    case CogMenu.menuItems.YELLOW:
                        t.text = "System";
                        break;
                }
            } else {
                image.enabled = true;
            }
        }
    }
}
