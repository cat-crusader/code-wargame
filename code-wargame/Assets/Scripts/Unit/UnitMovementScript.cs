using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovementScript : MonoBehaviour
{
    [SerializeField] Unit unit;
    private NavMeshAgent navMeshAgent;
    private string MovementType;

    public Vector3 Destination;
    public Vector3 TempDestination;


  
    public Vector3 pos;
    public bool moving;
    // Start is called before the first frame update
    void Awake()
    {
        moving = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (navMeshAgent != null && moving)
        {
            Moving();
            if (isNear(Destination, 0.1f)) moving = false;
        }
        //if (moving)
        //{
        //    Moving();
        //    if (isNear(Destination, 0.1f)) moving = false;
        //    //if (gameObject.transform.position.x - Destination.x <= 0.1 && gameObject.transform.position.x - Destination.x >= -0.1) moving = false;
        //}
    }
    bool isNear(Vector3 Target, float proximity)
    {
        if (Vector3.Distance(transform.position,Destination) < proximity) return true;
        return false;
    }
    public void Moving()
    {
        navMeshAgent.destination = TempDestination;
        //transform.Translate(MoveVector);
    }
    public void MoveTo(Vector3 toPos)
    {
        Destination = toPos;
        TempDestination = Destination;
        //pos = gameObject.transform.position;
        //MoveVector = Vector3.Normalize(toPos - pos);
        //MoveVector /= 10;
        moving = true;
    }
    public void FastMoveTo(Vector3 toPos)
    {
        Destination = toPos;

    }
    public Curve GetNearestRoadSegment()// !!!not work
    {
        Curve nearestCurve=new Curve();
        foreach(Curve curve in unit.roadSystem.curves)
        {
            if (nearestCurve == null)
            {
                //nearestCurve = curve;
            }
        }
        //unit.roadSystem;
        return new Curve();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(Destination, 0.3f);
    }
}
