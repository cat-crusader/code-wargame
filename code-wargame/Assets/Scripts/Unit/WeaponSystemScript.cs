using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystemScript : MonoBehaviour
{
    public Unit unit;

    
    public ParticleSystem ps;

    public List<WeaponSO> weapons;
    public List<float> weaponsCooldown;//in sec
    public List<bool> weaponsReadyToShoot;

    public Unit target;
    public bool isTargeted;


    public Ray LineOfSight;
    public Vector3 direction;
    private void Start()
    {
        target = null;
        isTargeted = false;

        weaponsCooldown = new List<float>();//coldown initiating
        for(int i = 0;i<weapons.Count;i++)
        {
            weaponsCooldown.Add(0f);
        }

        weaponsReadyToShoot = new List<bool>();//ready to shoot initiating
        for (int i = 0; i < weapons.Count; i++)
        {
            weaponsReadyToShoot.Add(true);
        }
        
        StartCoroutine(DoCheck());
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weaponsReadyToShoot[i]) continue;
            if (!isNear(0,weaponsCooldown[i],0.1f)) 
            { weaponsCooldown[i] -= Time.deltaTime; }
            if (isNear(0, weaponsCooldown[i], 0.1f))
            { weaponsReadyToShoot[i] = true; }
        }
    }
    public bool isNear(float num,float numToCompare, float eps)
    {
        //Debug.Log("is near");
        if (num-eps<numToCompare && numToCompare<num + eps) return true;
        return false;
    }
    public void AutoTargeting()
    {
        Collider[] hitColliders = Physics.OverlapSphere(unit.gameObject.transform.position, 3);
        isTargeted = false;
        

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponentInParent<Unit>() == null) continue;

            
            if (target == null && hitCollider.gameObject.GetComponentInParent<Unit>().player.type == "enemy" && unit.player.type!="enemy")
            {
                target = hitCollider.gameObject.GetComponentInParent<Unit>();
                
            }
            if (hitCollider.gameObject.GetComponentInParent<Unit>() == target) isTargeted = true;
        }
        if (isTargeted == false)
        {
            target = null;
            //ps.Stop();
        }
    }
    public void EngageTarget()
    {
        

        direction = target.gameObject.transform.position - unit.gameObject.transform.position;
        LineOfSight = new Ray(unit.gameObject.transform.position, direction.normalized);

        ps.transform.rotation = Quaternion.LookRotation(direction);

        // ¬изначаЇмо в≥дстань м≥ж точками
        float distance = direction.magnitude;
        for(int i = 0; i < weapons.Count; i++)
        {
            if (weaponsReadyToShoot[i] == true)
            {
                RaycastHit[] hits = Physics.RaycastAll(LineOfSight, weapons[i].Range);
                Shoot(weapons[i]);
                //if(weapons[i].Damage>=2)Instantiate()
                WeaponCooldown(weapons[i],i);
            }
        }
        //foreach (WeaponSO weapon in weapons)
        //{
        //    // ¬иконуЇмо пром≥нь ≥з перев≥ркою на перетин з об'Їктами
        //    RaycastHit[] hits = Physics.RaycastAll(LineOfSight, weapon.Range);
        //    Shoot(weapon);
        //}

    }
    public void Shoot(WeaponSO weapon)
    {

        ps.Play();
        unit.fireControl.FireShot(unit, target, weapon);
        
    }
    public void WeaponCooldown(WeaponSO weapon,int i) 
    {
        weaponsCooldown[i] = 60f/ (float)weapon.RoundsPerMinute;
        weaponsReadyToShoot[i] = false;
    }
    IEnumerator DoCheck()
    {
        for (; ; )
        {
            AutoTargeting();
            if (target != null) EngageTarget();
            yield return new WaitForSeconds(1f);
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawRay(unit.gameObject.transform.position, direction.normalized*3);
    }
}
