using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeshFromBezier : MonoBehaviour
{
    public int detail = 100;

    public Mesh mesh;

    private void OnDrawGizmos()
    {
        if (mesh == null)
        {
            mesh = new Mesh();
            GetComponent<UnityEngine.MeshFilter>().sharedMesh = mesh;
            GetComponent<UnityEngine.MeshFilter>().mesh = mesh;
        }
        Vector3[] vertices = new Vector3[2*detail];
        int[] triangles = new int[2*detail*3];  
        Vector3[] normals = new Vector3[2*detail];
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
            vertices[2 * t] = bezierPoints[t] + bezierBinormals[t];
            vertices[2 * t + 1] = bezierPoints[t] - bezierBinormals[t];
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

        for (int i = 0; i < detail; i+= 2)
        {
            triangles[6*i] = i;
            triangles[6*i+1] = i+1;
            triangles[6*i+2] = i+2;
            
            triangles[6*i+3] = i+1;
            triangles[6*i+4] = i+2;
            triangles[6*i+5] = i+3;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        // mesh.mat = uvs;
        mesh.RecalculateNormals();
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