using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    public UnitWeaponScript weapon;

    [SerializeField]
    public UnitReconScript recon;

    [SerializeField]
    public UnitMovementScript movement;

    public Player player;
    public string Team;

    public bool isVisible;

    public void SetVisible()
    {
        isVisible = true;
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        StartCoroutine(DoCheck());
    }
    public void SetInvisible()
    {
        isVisible = false;
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        StopCoroutine(DoCheck());
    }

    public void Start()
    {
       if(Team=="enemy") SetInvisible();
    }
    IEnumerator DoCheck()
    {

       yield return new WaitForSeconds(1f);
        SetInvisible(); 
    }

}
