using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coil : MonoBehaviour
{
    public int spins = 3;
    public int height = 3;
    public int radius = 3;
    public int detail = 100;
    public Vector2 AngToDir(float dir) => new Vector2(Mathf.Cos(dir), Mathf.Sin(dir));


    private void OnDrawGizmos()
    {
        // Gizmos.DrawWireSphere(default, 1.0f);
        Gizmos.DrawSphere(default, 0.3f);
        // Gizmos.DrawSphere(Vector3.Slerp(Vector3.right, Vector3.up, 0.5f), 0.3f);
        // Gizmos.DrawSphere(Vector3.Slerp(Vector3.right, -Vector3.up, 0.5f), 0.3f);
        // Gizmos.DrawSphere(Vector3.Slerp(Vector3.right, -Vector3.right, 0.5f), 0.3f);
        // Debug.Log(Vector3.Slerp(Vector3.right, -Vector3.right, 0.5f));
        Gizmos.DrawSphere(Vector3.Slerp(Vector3.right, AngToDir(179 * Mathf.Deg2Rad), 0.5f), 0.3f);
        Gizmos.DrawSphere(Vector3.Slerp(Vector3.right, AngToDir(181 * Mathf.Deg2Rad), 0.5f), 0.3f);

        Vector3 firstCheckPoint = AngToDir(179 * Mathf.Deg2Rad);
        Vector3 secondCheckPoint = AngToDir(181 * Mathf.Deg2Rad);

        Vector3 prevPoint = Vector3.right;
        Vector3 nextPoint = Vector3.right;
        for (int i = 0; i < detail; i++)
        {
            if (i < detail / 2)
            {
                nextPoint = Vector3.Slerp(Vector3.right, firstCheckPoint, (2f * i) / detail);
                Gizmos.DrawLine(prevPoint, nextPoint);
                prevPoint = nextPoint;
            }
            else if (i > detail / 2) //if == 180 ignore(edgecase)
            {
                nextPoint = Vector3.Slerp(Vector3.right, secondCheckPoint, 1f - ((i - detail / 2f) / (detail / 2f)));
                Gizmos.DrawLine(prevPoint, nextPoint);
                prevPoint = nextPoint;
            }
        }
        // Gizmos.DrawSphere(Vector3.Slerp(Vector3.right, AngToDir(359 * Mathf.Deg2Rad), 0.5f), 0.3f);
    }
}