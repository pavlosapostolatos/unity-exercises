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
        Debug.Log(Mathf.Atan2(0.0f, 1) * Mathf.Rad2Deg);
        float maxAngle = 0f;
        Camera cam = GetComponent<Camera>();
        foreach (Transform point in points)
        {
            Gizmos.DrawWireSphere(point.position, radius);
            Vector3 relPos = cam.transform.InverseTransformPoint(point.position);
            maxAngle = MaxAngleIn2dSpace(cam, maxAngle,relPos);
            maxAngle = MaxAngleIn2dSpace(cam, maxAngle,new Vector2(relPos.z,relPos.y));
        }

        Gizmos.color = Color.red;
        Vector3 dir = Quaternion.AngleAxis(maxAngle, Vector3.forward) * Vector3.right;
        Gizmos.DrawLine(cam.transform.position, cam.transform.position + dir * 100);
        dir = Quaternion.AngleAxis(-maxAngle, Vector3.forward) * Vector3.right;
        Gizmos.DrawLine(cam.transform.position, cam.transform.position + dir * 100);
        // always positive of the right vector and moving along the up vector

        dir = Quaternion.AngleAxis(maxAngle, Vector3.up) * Vector3.right;
        Gizmos.DrawLine(cam.transform.position, cam.transform.position + dir * 100);
        dir = Quaternion.AngleAxis(-maxAngle, Vector3.up) * Vector3.right;
        Gizmos.DrawLine(cam.transform.position, cam.transform.position + dir * 100);
        // always positive of the right vector and moving along the forward vector
    }

    private float MaxAngleIn2dSpace(Camera cam, float maxAngle ,Vector2 point)
    {
        float opposite = Mathf.Abs(point.y);
        float adjacent = point.x;

        float angle = Mathf.Atan2(opposite, adjacent) * Mathf.Rad2Deg;

        float extraAngleWithRadius = Mathf.Asin(radius / point.magnitude) * Mathf.Rad2Deg;
        angle += extraAngleWithRadius;

        maxAngle = Mathf.Max(Mathf.Abs(angle), maxAngle);

        cam.fieldOfView = maxAngle;

        //draw an infinite line based on an angle

        return maxAngle;
    }
}