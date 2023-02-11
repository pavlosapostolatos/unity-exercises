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
    public Vector3 AngToDir(float dir) => new Vector3(Mathf.Cos(dir), 0, Mathf.Sin(dir));


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(default, Vector3.up * height * spins);
        Gizmos.DrawSphere(default, 0.3f);

        Vector3 firstCheckPoint = AngToDir(179 * Mathf.Deg2Rad);
        Vector3 secondCheckPoint = AngToDir(181 * Mathf.Deg2Rad);

        Vector3 prevPoint = Vector3.right;
        Vector3 nextPoint;
        for (int spin = 0; spin < spins; spin++)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(Vector3.right * radius + Vector3.up * height * spin, 0.3f); //signify start of new spin
            for (int i = 0; i < detail; i++)
            {
                if (i < detail / 2)
                    nextPoint = radius * Vector3.Slerp(Vector3.right, firstCheckPoint, (2f * i) / detail);
                else
                {
                    nextPoint = radius * Vector3.Slerp(Vector3.right, secondCheckPoint,
                        1f - ((i - detail / 2f) / (detail / 2f)));
                }

                Gizmos.color = Color.white;
                nextPoint.y = spin * height + (float)i / detail * height;
                Gizmos.DrawLine(prevPoint, nextPoint);
                prevPoint = nextPoint;
            }
        }
    }
}