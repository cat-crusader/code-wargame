using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovementScript : MonoBehaviour
{
    [SerializeField] Unit unit;

    [SerializeField]
    public RoadSystem roadSystem;

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
        //NavMeshHit navMeshHit;
        //navMeshAgent.SamplePathPosition(NavMesh.AllAreas, 0f, out navMeshHit);
        //if (navMeshHit.mask == "Forest")
        //{

        //}
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
        if (unit.unitStats.type == UnitType.Infantry)
        {
            MoveTo(toPos);
            return;
        }
        Destination = toPos;

        float timeToTravel = CalculateTimeToTravel(toPos, unit.unitStats.speed);
        Debug.Log(timeToTravel + " time to travel");
        Curve nearestRoadToUnit = GetNearestRoadSegment(unit.gameObject.transform.position);
        Curve nearestRoadToTarget = GetNearestRoadSegment(toPos);
        TempDestination = GetNearestRoadPosition(unit.gameObject.transform.position, nearestRoadToUnit, 10);

        moving = true;
    }
    public float CalculateTimeToTravel(Vector3 toPos, float speed)
    {
        float Distance = Vector3.Distance(unit.gameObject.transform.position, toPos);
        return Distance / speed *10;
    }
    public Vector3 GetNearestRoadPosition(Vector3 from, Curve curve, float accuracy)
    {
        float distance = Mathf.Infinity;
        float minDistance = Mathf.Infinity;
        Vector3 nearestPoint = Vector3.zero;
        for (int i = 0; i < accuracy; i++)
        {
            distance = Vector3.Distance(from, curve.GetCurve(i / accuracy));
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPoint = curve.GetCurve(i / accuracy);
            }
        }
        return nearestPoint;
    }
    public Curve GetNearestRoadSegment(Vector3 from)// !!!not work
    {
        Curve nearestCurve=null;
        float distance = Mathf.Infinity;
        float minDistance = Mathf.Infinity;
        foreach (Curve curve in roadSystem.curves)
        {
            distance = CalculateAverageDistanceToCurve(from, curve);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestCurve = curve;
            }
        }
        //unit.roadSystem;
        return nearestCurve;
    }
    public float CalculateAverageDistanceToCurve(Vector3 pos, Curve curve)
    {
        List<Vector3> points = new List<Vector3>();
        points.Add(curve.start.position);
        points.Add(curve.end.position);
        for (int i = 0; i < curve.points.Count; i++)
            points.Add(curve.points[i].position);

        Vector3 average = GetAverageVector(points);

        Debug.Log(average);
        return Vector3.Distance(pos,average);
    }
    public Vector3 GetAverageVector(List<Vector3> list)
    {
        Vector3 average = Vector3.zero;
        for (int i = 0; i < list.Count; i++)
        {
            average += list[i];
        }
        return average /= list.Count;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(Destination, 0.3f);
    }
}
