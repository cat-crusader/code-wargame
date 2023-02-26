using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystemScript : MonoBehaviour
{
    public Unit unit;
    public List<Weapon> weapons;
    public Unit target;


    public Ray LineOfSight;
    public Vector3 direction;
    private void Start()
    {
        target = null;
        StartCoroutine(DoCheck());
    }

    public void AutoTargeting()
    {
        Collider[] hitColliders = Physics.OverlapSphere(unit.gameObject.transform.position, 2);

        

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponentInParent<Unit>() == null) continue;

            if (target == null && hitCollider.gameObject.GetComponentInParent<Unit>().Team =="enemy") target = hitCollider.gameObject.GetComponentInParent<Unit>();
        }
    }
    public void EngageTarget()
    {


        direction = target.gameObject.transform.position - unit.gameObject.transform.position;
        LineOfSight = new Ray(unit.gameObject.transform.position, direction.normalized);
     

        // ¬изначаЇмо в≥дстань м≥ж точками
        float distance = direction.magnitude;

        // ¬иконуЇмо пром≥нь ≥з перев≥ркою на перетин з об'Їктами
        RaycastHit[] hits = Physics.RaycastAll(LineOfSight, 2);

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
        Gizmos.DrawRay(unit.gameObject.transform.position, direction.normalized*2);
    }
}
