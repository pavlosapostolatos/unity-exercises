using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public Transform center;

    public int n;
    public int size;
    public int d;

    public Vector2 AngToDir(float dir) => new Vector2(Mathf.Cos(dir), Mathf.Sin(dir));

    public Vector2 posOnShape(Vector2 pos) => (Vector2)center.position + size * pos;

    public void OnDrawGizmos()
    {
        Vector2[] array = new Vector2[n];
        float step = 360f / (float)n;
        for (int i = 0; i < n; i++)
        {
            Vector2 pos = AngToDir(step * i * Mathf.Deg2Rad);
            array[i] = pos;
            Gizmos.DrawSphere(posOnShape(pos), 0.5f);
        }

        for (int i = 0; i < n; i++)
        {
            Gizmos.DrawLine(posOnShape(array[i]), posOnShape(array[(i + d) % n]));
        }
    }
}