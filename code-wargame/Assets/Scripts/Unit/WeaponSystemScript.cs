using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystemScript : MonoBehaviour
{
    public Unit unit;

    
    public ParticleSystem ps;

    public List<WeaponSO> weapons;
    public Unit target;
    public bool isTargeted;


    public Ray LineOfSight;
    public Vector3 direction;
    private void Start()
    {
        target = null;
        isTargeted = false;
        //ps.Play();
        StartCoroutine(DoCheck());
    }

    public void AutoTargeting()
    {
        Collider[] hitColliders = Physics.OverlapSphere(unit.gameObject.transform.position, 3);//change targeting to smaller
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
            ps.Stop();
        }
    }
    public void EngageTarget()
    {
        

        direction = target.gameObject.transform.position - unit.gameObject.transform.position;
        LineOfSight = new Ray(unit.gameObject.transform.position, direction.normalized);

        ps.transform.rotation = Quaternion.LookRotation(direction);

        // ��������� ������� �� �������
        float distance = direction.magnitude;

        foreach (WeaponSO weapon in weapons)
        {
            // �������� ������ �� ��������� �� ������� � ��'������
            RaycastHit[] hits = Physics.RaycastAll(LineOfSight, weapon.Range);
            Shoot(weapon);
        }

    }
    public void Shoot(WeaponSO weapon)
    {

        ps.Play();

        
        
        if(weapon.Accuracy > Random.Range(0f, 100.0f))
        {
            target.TakeDamage(1);
            Debug.Log(this.name +" hitted "+ target.name);
        }

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
