using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class TriangleCollision : MonoBehaviour
{
    public Vector3 A;
    public Vector3 B;
    public Vector3 C;
    
    public void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Vector3 AB = B - A;
        Vector3 AC = C - A;
        Vector3 BA = A - B;
        Vector3 BC = C - B;
        Vector3 CA = A - C;
        Vector3 CB = B - C;
        
        
        float TotalArea = Vector3.Cross(AB, AC).magnitude/2;
        
        float Area1 = Vector3.Cross(pos - A, pos - B).magnitude/2;
        float Area2 = Vector3.Cross(pos - B, pos - C).magnitude/2;
        float Area3 = Vector3.Cross(pos - C, pos - A).magnitude/2;
        if (Math.Abs(Area1 + Area2 + Area3 - TotalArea) < 0.01f)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawLine(A, B);
        Gizmos.DrawLine(B, C);
        Gizmos.DrawLine(C, A);
        Gizmos.DrawSphere(A, 0.1f);
        Gizmos.DrawSphere(B, 0.1f);
        Gizmos.DrawSphere(C, 0.1f);
    }

}
