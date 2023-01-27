using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{
    public Transform[] points;

    [Range(0f, 5f)] public float radius = 0.5f;

    public void OnDrawGizmos()
    {
        float maxAngle = 0f;
        Camera cam = GetComponent<Camera>();

        foreach (Transform point in points)
        {
            Gizmos.DrawWireSphere(point.position,radius);
            
            Vector2 relPos = point.position - cam.transform.position;

            float opposite = Mathf.Abs(relPos.y);
            float adjacent = relPos.x;

            float angle = Mathf.Atan2(opposite, adjacent) * Mathf.Rad2Deg;

            float extraAngleWithRadius = Mathf.Asin(radius / relPos.magnitude) * Mathf.Rad2Deg;
            angle += extraAngleWithRadius;
            
            maxAngle = Mathf.Max(Mathf.Abs(angle), maxAngle);

            cam.fieldOfView = maxAngle;

            //draw an infinite line based on an angle
        }
        
        Gizmos.color = Color.red;
        Vector3 dir = Quaternion.AngleAxis(maxAngle, Vector3.forward) * Vector3.right;
        Gizmos.DrawLine(cam.transform.position, cam.transform.position + dir * 100);
        dir = Quaternion.AngleAxis(-maxAngle, Vector3.forward) * Vector3.right;
        Gizmos.DrawLine(cam.transform.position, cam.transform.position + dir * 100);
    }
}