using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UnitScriptableObject", order = 2)]
public class UnitSO : ScriptableObject
{
    public new string name;
    public int speed;
    public int hp;
    public UnitType type;
}
