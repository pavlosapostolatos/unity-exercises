using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSurfaceArea : MonoBehaviour
{
    
    public Mesh mesh;

    public void OnDrawGizmos()
    {
        Debug.Log(mesh.triangles.Length);
        Debug.Log(mesh.vertices.Length);

        float SurfaceArea = 0;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 p1 = mesh.vertices[mesh.triangles[i]];
            Vector3 p2 = mesh.vertices[mesh.triangles[i + 1]];
            Vector3 p3 = mesh.vertices[mesh.triangles[i + 2]];

            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p1);

            SurfaceArea += Vector3.Cross(p2 - p1, p3 - p1).magnitude / 2;
        }
        Debug.Log(SurfaceArea);
    }
}
