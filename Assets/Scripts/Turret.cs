using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform turret;

    public void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Gizmos.DrawLine(transform.position, hit.point);
            turret.position = hit.point;

            Vector3 yAxis = hit.normal;

            Vector3 xAxis = Vector3.Cross(yAxis, ray.direction);

            Vector3 zAxis = Vector3.Cross(xAxis, yAxis);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(hit.point, xAxis);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(hit.point, yAxis);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(hit.point, zAxis);
            
            turret.rotation = Quaternion.LookRotation(zAxis, yAxis);
        }
    }
}