using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tatsu
{
    [CreateAssetMenu(fileName = "TatsuWeapon", menuName = "Tatsu/Weapon")]
    public class TatsuWeapon : ScriptableObject {
        public TatsuClass weaponClass;
        public Sprite icon;
        public int baseDamage = 1;
        public int baseDefense = 1;
        public int baseSpeed = 1;
        public int baseAgility = 1;       
    }
}