using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponScriptableObject", order = 1)]
public class WeaponSO : ScriptableObject
{
    public float Range;
    public float Accuracy;
    public float Damage;
    public DamageType damageType;
    public int RoundsPerMinute;
}


