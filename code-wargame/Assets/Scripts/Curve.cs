using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    [SerializeField] private GameObject pointPrefab;
    public Transform result;
    private float interpolateAmount;


    [SerializeField] private List<Transform> points;



    // Update is called once per frame
    void Update()
    {
        //interpolateAmount = (interpolateAmount + Time.deltaTime) % 1;

        //if(points.Count == 0)
        //    result.position = Vector3.Lerp(start.position, end.position, interpolateAmount);
        //if (points.Count == 1)
        //    result.position = QuadraticLerp(start.position, points[0].position, end.position, interpolateAmount);
        //if (points.Count == 2)
        //    result.position = CubicLerp(start.position,points[0].position,points[1].position,end.position,interpolateAmount);

    }
    public Vector3 GetCurve(float interpolate)
    {
 
        if (points.Count == 1)
            return QuadraticLerp(start.position, points[0].position, end.position, interpolate);
        if (points.Count == 2)
            return CubicLerp(start.position, points[0].position, points[1].position, end.position, interpolate);

        return Vector3.Lerp(start.position, end.position, interpolate);
    }
    public void AlignPointPosition()
    {
        for(int i = 0; i < points.Count; i++)
        {
            points[i].position = Vector3.Lerp(start.position, end.position, (i + 1) / (float)(points.Count +1));
        }
    }
    public void AddPoint()
    {
        if (points.Count < 2)
        {


            points.Add(Instantiate(pointPrefab, transform).transform);
            AlignPointPosition();
        }
    }
    public void RemovePoint()
    {
        if (points.Count > 0)
        {

            DestroyImmediate(points[points.Count - 1].gameObject);
            points.RemoveAt(points.Count - 1);
            AlignPointPosition();
        }
    }
    private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(ab,bc,t);
    }
    private Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c,Vector3 d, float t)
    {
        Vector3 ab_bc = QuadraticLerp(a, b,c, t);
        Vector3 bc_cd = QuadraticLerp(b, c, d, t);
        return Vector3.Lerp(ab_bc, bc_cd, t);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (points.Count == 0)
        {
            Gizmos.DrawLine(start.position, end.position);

        }
        if (points.Count == 1)
        {
            Gizmos.DrawLine(start.position, points[0].position);

            Gizmos.DrawLine(points[0].position, end.position);
        }
        if (points.Count == 2)
        {
            Gizmos.DrawLine(start.position, points[0].position);
            Gizmos.DrawLine(points[0].position, points[1].position);
            Gizmos.DrawLine(points[1].position, end.position);
        }

        Gizmos.DrawSphere(start.position, 0.2f);
        Gizmos.DrawSphere(end.position, 0.2f);
    }

}
 