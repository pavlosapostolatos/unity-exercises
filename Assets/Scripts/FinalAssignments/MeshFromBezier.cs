using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeshFromBezier : MonoBehaviour
{
    public int detail = 100;

    private void OnDrawGizmos()
    {
        Vector3[] points = new Vector3[transform.childCount];
        for (int i = 0; i < points.Length; i++)
            points[i] = transform.GetChild(i).position;

        Vector3[] bezierPoints = new Vector3[detail];
        Vector3[] bezierTangents = new Vector3[detail];
        Vector3[] bezierNormals = new Vector3[detail];
        Vector3[] bezierBinormals = new Vector3[detail];
        for (int t = 0; t < detail; t++)
        {
            Tuple<Vector3, Vector3> tuple = getBezierPoint(points, t);
            bezierPoints[t] = tuple.Item1;
            bezierTangents[t] = tuple.Item2.normalized;
            bezierNormals[t] = new Vector3(- bezierTangents[t].y,bezierTangents[t].x);
            bezierBinormals[t] = Vector3.Cross(bezierTangents[t],bezierNormals[t]);
            if (t % 10 == 0)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(bezierPoints[t], bezierTangents[t]);
                Gizmos.color = Color.green;
                Gizmos.DrawRay(bezierPoints[t], bezierNormals[t]);
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(bezierPoints[t], bezierBinormals[t]);
            }
        }

        Handles.DrawAAPolyLine(bezierPoints);
    }

    private Tuple<Vector3, Vector3> getBezierPoint(Vector3[] points, int t)
    {
        int length = points.Length;
        while (length > 1)
        {
            for (int i = 0; i < length - 1; i++)
            {
                Vector3 lerp = Vector3.Lerp(points[i], points[i + 1], (float)t / detail);
                if (length == 2)
                {
                    return new Tuple<Vector3, Vector3>(lerp, points[i + 1] - points[i]);
                }

                points[i] = lerp;
            }

            length--;
        }

        throw new NotImplementedException("This should never happen");
    }
}


// public Mesh mesh;
// public Material material;
// public Vector3[] vertices;
// public int[] triangles;
// public Vector2[] uvs;
//
// void Start()
// {
//    mesh = new Mesh();
//    GetComponent<UnityEngine.MeshFilter>().mesh = mesh;
// }
//
// void Update()
// {
//    mesh.Clear();
//    mesh.vertices = vertices;
//    mesh.triangles = triangles;
//    mesh.uv = uvs;
//    mesh.RecalculateNormals();
// }