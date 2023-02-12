using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        Gizmos.matrix = transform.localToWorldMatrix;
        Handles.matrix = transform.localToWorldMatrix;
        drawUpstandingCoil();
        drawTorusCoil();


        void drawUpstandingCoil()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(default, Vector3.up * height * spins);
            Gizmos.DrawSphere(default, 0.3f);

            Vector3 firstCheckPoint = AngToDir(179 * Mathf.Deg2Rad);
            Vector3 secondCheckPoint = AngToDir(181 * Mathf.Deg2Rad);

            Vector3 prevPoint = Vector3.right;
            Vector3 nextPoint;
            float colorIndex = 0;
            for (int spin = 0; spin < spins; spin++)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(Vector3.right * radius + Vector3.up * height * spin,
                    0.3f); //signify start of new spin
                for (int i = 0; i < detail; i++, colorIndex++)
                {
                    if (i < detail / 2)
                        nextPoint = radius * Vector3.Slerp(Vector3.right, firstCheckPoint, (2f * i) / detail);
                    else
                    {
                        nextPoint = radius * Vector3.Slerp(Vector3.right, secondCheckPoint,
                            1f - ((i - detail / 2f) / (detail / 2f)));
                    }

                    Gizmos.color = Color.Lerp(Color.red, Color.blue, colorIndex / (detail * spins));
                    nextPoint.y = spin * height + (float)i / detail * height;
                    Gizmos.DrawLine(prevPoint, nextPoint);
                    prevPoint = nextPoint;
                }
            }
        }

        void drawTorusCoil()
        {
            float circumference = height * spins;
            Gizmos.color = Color.magenta;
            // Gizmos.DrawLine(default, Vector3.up * height * spins);
            float majorRadius = circumference / (2 * Mathf.PI);
            Handles.DrawWireDisc(Vector3.zero, Vector3.up, majorRadius);
            float colorIndex = 0;
            Vector3 prevPoint = Vector3.right;
            Vector3 nextPoint;
            // int spin = 0;
            for (int spin = 0; spin < spins; spin++)
            {
                for (int i = 0; i < detail; i++, colorIndex++)
                {
                    float angleInTorus = (spin + (i / (float)detail)) * 2 * Mathf.PI / spins;
                    Vector3 dirInTorus = AngToDir(angleInTorus);
                    Vector3 pointInTorus = dirInTorus * majorRadius;

                    float angleInSpin = 2 * Mathf.PI * (i / (float)detail);
                    Vector3 dirInSpin = AngToDir(angleInSpin);
                    Vector3 pointInSpin = dirInSpin * radius;
                    pointInSpin.y = pointInSpin.z;

                    // if (i < detail / 4)
                    // {
                    //     pointInSpin.y = Mathf.InverseLerp(0,detail/4,i);
                    // }
                    // else if (i < detail / 4)
                    // {
                    //     pointInSpin.y = 1 - Mathf.InverseLerp(detail/4,detail/2,i);
                    // }
                    // else if (i < 3 * detail / 4)
                    // {
                    //     pointInSpin.y = - Mathf.InverseLerp(detail/2,3*detail/4,i);
                    // }
                    // else
                    // {
                    //     pointInSpin.y = -1 + Mathf.InverseLerp(3 *detail/4,detail,i);
                    // }
                    // pointInSpin.y *= radius;
                    nextPoint = pointInTorus + pointInSpin;

                    Gizmos.color = Color.Lerp(Color.red, Color.blue, colorIndex / (detail * spins));
                    Gizmos.DrawLine(prevPoint, nextPoint);
                    prevPoint = nextPoint;
                }
            }
        }
    }
}