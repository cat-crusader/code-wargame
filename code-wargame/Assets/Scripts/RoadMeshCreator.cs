using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMeshCreator : MonoBehaviour
{
    [SerializeField] RoadSystem roadSystem;
    public int length = 100;
    //[SerializeField] List<Curve> curves;
    [SerializeField] GameObject MeshPrefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Curve curve in roadSystem.curves)
        {
            CreateRoadMesh(curve);
        }
    }
    public void CreateRoadMesh(Curve curve)
    {
        Mesh mesh = new Mesh();

        Vector3[] verticies = new Vector3[4 * length];
        Vector2[] uv = new Vector2[4*length];
        int[] trianges = new int[6*length];

        for (int i = 0; i < length; i++)
        {
            Vector3 curvePoint = curve.GetCurve(i / (float)length);
            Vector3 nextCurvePoint = curve.GetCurve((i+1) / (float)length);

            Vector3 normal = GetNormal(i,curve);
            Vector3 nextNormal = GetNormal(i + 1,curve);

            Vector3 clampedNormal = Vector3.ClampMagnitude(normal, 1f);
            Vector3 nextClampedNormal = Vector3.ClampMagnitude(nextNormal, 1f);

            //Debug.Log(i / (float)length);
            verticies[0 + i*4] = new Vector3(curvePoint.x, curvePoint.y, curvePoint.z);
            verticies[1 + i*4] = new Vector3(nextCurvePoint.x, nextCurvePoint.y, nextCurvePoint.z);
            verticies[2 + i*4] = new Vector3(nextCurvePoint.x+nextClampedNormal.x, nextCurvePoint.y+nextClampedNormal.y,nextCurvePoint.z+nextClampedNormal.z);
            verticies[3 + i*4] = new Vector3(curvePoint.x+clampedNormal.x, curvePoint.y + clampedNormal.y, curvePoint.z + clampedNormal.z);
            Debug.Log(verticies[0 + i * 4] +":" +verticies[1 + i * 4] + ":" + verticies[2 + i * 4] + ":" + verticies[3 + i * 4]);
            //verticies[0+i] = new Vector3(0, 0, 0);
            //verticies[1+i] = new Vector3(0, 0, 1);
            //verticies[2+i] = new Vector3(1, 0, 1);
            //verticies[3+i] = new Vector3(1, 0, 0);


            uv[0+i*4] = new Vector2(0, 0);
            uv[1+i*4] = new Vector2(0, 1);
            uv[2+i*4] = new Vector2(1, 1);
            uv[3+i*4] = new Vector2(1, 0);

            //front
            //trianges[0 + i * 6] = 0 + i * 4;
            //trianges[1 + i * 6] = 1 + i * 4;
            //trianges[2 + i * 6] = 2 + i * 4;

            //trianges[3 + i * 6] = 0 + i * 4;
            //trianges[4 + i * 6] = 2 + i * 4;
            //trianges[5 + i * 6] = 3 + i * 4;

            //back
            trianges[0 + i * 6] = 0 + i * 4;
            trianges[1 + i * 6] = 2 + i * 4;
            trianges[2 + i * 6] = 1 + i * 4;

            trianges[3 + i * 6] = 0 + i * 4;
            trianges[4 + i * 6] = 3 + i * 4;
            trianges[5 + i * 6] = 2 + i * 4;
        }

        mesh.vertices = verticies;
        mesh.uv = uv;
        mesh.triangles = trianges;

        GameObject roadMesh = Instantiate(MeshPrefab, transform);
        roadMesh.GetComponent<MeshFilter>().mesh = mesh;
    }
    public Vector3 GetNormal(int i, Curve curve)
    {
        float epsilon = 0.001f; // Adjust epsilon value as needed
        Vector3 positionPlusEpsilon = curve.GetCurve((i + epsilon) / (float)length);
        Vector3 positionMinusEpsilon = curve.GetCurve((i - epsilon) / (float)length);
        Vector3 tangent = (positionPlusEpsilon - positionMinusEpsilon).normalized;

         return  Vector3.Cross(tangent, Vector3.up);
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        foreach (Curve curve in roadSystem.curves)
        {
            for (int i = 0; i < 100; i++)
            {
                Vector3 curvePoint = curve.GetCurve(i / (float)length);

                float epsilon = 0.001f; // Adjust epsilon value as needed
                Vector3 positionPlusEpsilon = curve.GetCurve((i + epsilon) / (float)length);
                Vector3 positionMinusEpsilon = curve.GetCurve((i - epsilon) / (float)length);
                Vector3 tangent = (positionPlusEpsilon - positionMinusEpsilon).normalized;

                Vector3 normal = Vector3.Cross(tangent, Vector3.up);
                Gizmos.DrawLine(curvePoint, curvePoint + Vector3.ClampMagnitude(normal, 1f));
            }
        }
    }

}
