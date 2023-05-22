using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UnitScriptableObject", order = 2)]
public class UnitSO : ScriptableObject
{
    public new string name;
    public int speed;
    public int roadSpeed;
    public int hp;
    public UnitType type;
    public ReconnaissanceType optics;
    public int frontalArmor;
    public int rearArmor;
    public int sideArmor;
    public int topArmor;
}
