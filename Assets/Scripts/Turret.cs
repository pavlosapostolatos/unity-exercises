using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class Turret : MonoBehaviour
{
    public Transform turret;

    public Transform gun1;
    
    public Transform gun2;

    public float height; //0.5

    public float distance; //0,.8 for my cube

    public float size1;
    
    public float size2;
  
    public float size3;

    
    Vector3[] corners = new Vector3[]{
        // bottom 4 positions:
        new Vector3( 1, 0, 1 ),
        new Vector3( -1, 0, 1 ),
        new Vector3( -1, 0, -1 ),
        new Vector3( 1, 0, -1 ),
        // top 4 positions:
        new Vector3( 1, 2, 1 ),
        new Vector3( -1, 2, 1 ),
        new Vector3( -1, 2, -1 ),
        new Vector3( 1, 2, -1 ) 
    };

    
    public void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Gizmos.DrawLine(transform.position, hit.point);
            Vector3 yAxis = hit.normal;

            Vector3 xAxis = Vector3.Cross(yAxis, ray.direction);

            Vector3 zAxis = Vector3.Cross(xAxis, yAxis);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(hit.point, xAxis);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(hit.point, yAxis);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(hit.point, zAxis);
            
            turret.position = hit.point;
            turret.rotation = Quaternion.LookRotation(zAxis, yAxis);
            
            Matrix4x4 turretMatrix = Matrix4x4.TRS(hit.point, Quaternion.LookRotation(zAxis, yAxis), Vector3.one);
            
            // Gizmos.DrawWireCube(turret.position,);

            foreach (Vector3 corner in corners)
            {
                Gizmos.DrawSphere(turret.TransformPoint(corner),0.3f);
                
                // or
                // Vector3 cornerToWorld = turretMatrix.MultiplyPoint3x4(corner);
                // Gizmos.DrawSphere(cornerToWorld,0.3f);

            }

            gun1.position = turret.position + yAxis * height;
            gun1.position += xAxis * distance;
            gun1.localScale = new Vector3(size1, size2, size3);//should have 3 sizes
            
            gun2.position = turret.position + yAxis * height;
            gun2.position -= xAxis * distance;
            gun2.localScale = new Vector3(size1, size2, size3);//should have 3
        }
    }
}