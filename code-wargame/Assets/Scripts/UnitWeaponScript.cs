using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWeaponScript : MonoBehaviour
{
    public UnitScript unit;
    public float Range;
    public UnitScript Target;
    public bool shooting;
    //Transform[] array;

    // Start is called before the first frame update
    void Start()
    {
        shooting = false;
        Target = null;
    }

    void GetTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(unit.gameObject.transform.position, Range);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.gameObject.name);
            if (Target == null && hitCollider.gameObject.GetComponentInParent<UnitScript>() != null && hitCollider.gameObject.GetComponentInParent<UnitScript>() != unit)
            {
                Target = hitCollider.gameObject.GetComponentInParent<UnitScript>();
                shooting = true;
            }
            //hitCollider.SendMessage("AddDamage");
        }
    }
    public void Shoot()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        GetTarget();
        if (shooting) Shoot();
    }
}
