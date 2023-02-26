using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitReconScript : MonoBehaviour
{
    [SerializeField]
    public Unit unit;

    

    public float VisionRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool isVisible(Unit enemy)
    {
        //foreach (UnitScript e in spottedEnemyUnits)
        //{
        //    if (e == enemy) return true;
        //}
        return false;
    }
    public void Recon()
    {
        Collider[] hitColliders = Physics.OverlapSphere(unit.gameObject.transform.position, VisionRange);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.gameObject.name);
            if (hitCollider.gameObject.GetComponentInParent<Unit>().Team =="enemy")
            {
                // spottedEnemyUnits.Add(hitCollider.gameObject.GetComponentInParent<UnitScript>());

            }


        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
