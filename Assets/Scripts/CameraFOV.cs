using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{
    public Transform[] points;

    public void OnDrawGizmos()
    {
        float maxAngle = 0f;
        Camera cam = GetComponent<Camera>();

        foreach (Transform point in points)
        {
            Vector2 relPos = point.position - cam.transform.position;

            float opposite = relPos.y;
            float adjacent = relPos.x;

            float angle = Mathf.Atan2(opposite, adjacent) * Mathf.Rad2Deg;

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