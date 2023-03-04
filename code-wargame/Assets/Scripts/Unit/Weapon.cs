using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponScriptableObject", order = 1)]
    public class Weapon : ScriptableObject
    {
        public float Range;
        public float Accuracy;
    }

