using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Unit : MonoBehaviour
{
    [SerializeField]
    public GameObject GameManager;

    [SerializeField]
    public RoadSystem roadSystem;

    [SerializeField]
    public FireController fireControl;

    [SerializeField]
    public UnitSO unitStats;

    [SerializeField]
    public UnitReconScript recon;

    [SerializeField]
    public UnitMovementScript movement;

    [Header("Test")]
    public Player player;

    private NavMeshAgent navMeshAgent;

    public int temp_CurrentHP;

    public bool isVisible;


    public void SetVisible()
    {
        isVisible = true;
        if (player.type == "enemy") gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        StartCoroutine(DoCheck());
    }
    public void SetInvisible()
    {
        isVisible = false;
        if (player.type == "enemy") gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        StopCoroutine(DoCheck());
    }

    public void Start()
    {
        GameManager = GameObject.Find("GameManager");
        fireControl = GameManager?.GetComponent<FireController>();

        if (GetComponent<NavMeshAgent>() != null) navMeshAgent = GetComponent<NavMeshAgent>();
        if (player.type == "enemy") SetInvisible();
        temp_CurrentHP = unitStats.hp;
        navMeshAgent.speed = unitStats.speed / 10;
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
    public void TakeDamage(int damage)
    {

        //switch (unitStats.type)
        //{
        //    case UnitType.Infantry:
        //        CalculateInfantryDamage(damage, type);
        //        break;
        //    case UnitType.Vehicle:
        //        CalculateVehicleDamage(damage, type);
        //        break;
        //}
        temp_CurrentHP -= damage;
        if (temp_CurrentHP <= 0) Destroy();
    }
    public void CalculateInfantryDamage(int damage, DamageType type)
    {
        
        // infantry dont get penetration damage
    }
    public void CalculateVehicleDamage(int damage, DamageType type)
    {
        if (type == DamageType.Explosive)
        {

        }
        else if (type == DamageType.Penetration)
        {

        }
    }

    IEnumerator DoCheck()
    {

       yield return new WaitForSeconds(1f);
        SetInvisible(); 
    }

}
