using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    public Weapon weapon;

    [SerializeField]
    public UnitReconScript recon;

    [SerializeField]
    public UnitMovementScript movement;

    [Header("Test")]
    public Player player;
    public string Team;

    public int temp_CurrentHP;

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
        temp_CurrentHP = 10;
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
    public void TakeDamage(int damage)
    {
        temp_CurrentHP -= damage;
        if (temp_CurrentHP <= 0) Destroy();
    }
    IEnumerator DoCheck()
    {

       yield return new WaitForSeconds(1f);
        SetInvisible(); 
    }

}
