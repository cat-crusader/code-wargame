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

    public List<Unit> Recon()
    {
        List<Unit> visible = new List<Unit>();
        Collider[] hitColliders = Physics.OverlapSphere(unit.gameObject.transform.position, VisionRange);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.gameObject.name);

            
            if (hitCollider.gameObject.GetComponentInParent<Unit>() == null) continue;
            var recUnit = hitCollider.gameObject.GetComponentInParent<Unit>();

            if (recUnit.Team == "enemy" && recUnit.isVisible == false)
            {
                 visible.Add(hitCollider.gameObject.GetComponentInParent<Unit>());
                 recUnit.SetVisible();
            }


        }
        return visible;
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(unit.gameObject.transform.position, VisionRange);
    }
}
