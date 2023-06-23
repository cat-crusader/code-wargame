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


    public event EventHandler<OnHPChangeArgs> OnHPChange;
    public class OnHPChangeArgs : EventArgs
    {
        public int HP;  
    }
    public event EventHandler<OnVisibleChangeArgs> OnVisibleChange;
    public class OnVisibleChangeArgs : EventArgs
    {
        public bool isVisible;
    }

    public event EventHandler<OnUnitSpawnArgs> OnUnitSpawn;
    public class OnUnitSpawnArgs : EventArgs
    {
        public Unit unit;
        public bool spawn;
    }

    public void SetVisible()
    {
        if (isVisible == false)
        {
            isVisible = true;
            if (player.type == "enemy") transform.Find("Model").gameObject.SetActive(true);
            OnVisibleChange?.Invoke(this, new OnVisibleChangeArgs { isVisible = true });
            //if (player.type == "enemy") gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
            Debug.Log("Set Visible");
            StartCoroutine(DoCheck());
        }
    }
    public void SetInvisible()
    {
        if (isVisible == true)
        {
            isVisible = false;
            if (player.type == "enemy") transform.Find("Model").gameObject.SetActive(false);
            OnVisibleChange?.Invoke(this, new OnVisibleChangeArgs { isVisible = false});
            Debug.Log("Set Invisible");
            //if (player.type == "enemy") gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            StopCoroutine(DoCheck());
        }
    }

    public void Start()
    {
        //gameObject.AddComponent<UnitSymbolUIScript>();
        OnSpawn();
    }
    public void OnSpawn()
    {
        GameManager = GameObject.Find("GameManager");
        fireControl = GameManager?.GetComponent<FireController>();
        isVisible = true;
        
        if (GetComponent<NavMeshAgent>() != null) navMeshAgent = GetComponent<NavMeshAgent>();
        if (player?.type == "enemy") SetInvisible();
        temp_CurrentHP = unitStats.hp;
        navMeshAgent.speed = unitStats.speed / 10;


        Debug.Log("ON Unit SPAWN 1");
        OnUnitSpawn?.Invoke(this, new OnUnitSpawnArgs { unit = this ,spawn = true});
    }
    public void OnDestroy()
    {
        OnUnitSpawn?.Invoke(this, new OnUnitSpawnArgs { unit = this, spawn = false });
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
        //gameObject.SetActive(false);
    }
    public void TakeDamage(int damage)
    {

        temp_CurrentHP -= damage;
        OnHPChange?.Invoke(this, new OnHPChangeArgs { HP = temp_CurrentHP});

        if (temp_CurrentHP <= 0) Destroy();
    }


    IEnumerator DoCheck()
    {

       yield return new WaitForSeconds(1f);
        SetInvisible(); 
    }

}
