using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tatsu
{
    [CreateAssetMenu(fileName = "TatsuClass", menuName = "Tatsu/Class")]
    public class TatsuClass : ScriptableObject
    {
        public string classTitle = "title";
        public string classID = "000";

        #region attack objects
        public TatsuAction jab,
        airJab,
        forwardJab,
        upJab,
        strong,
        airStrong,
        forwardStrong,
        upStrong;        
        #endregion


    }
}