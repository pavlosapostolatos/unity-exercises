using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLocal : MonoBehaviour
{

    public Vector2 WorldTf;

    public Vector2 LocalTf;

    void OnDrawGizmos()
    {
        // WorldTf = LocalToWorld(LocalTf);
        // Gizmos.DrawSphere(WorldTf, 1.0f);

        LocalTf = WorldToLocal(WorldTf);
        Gizmos.DrawSphere(LocalTf, 1.0f);
    }

    public Vector2 LocalToWorld(Vector2 localTf)
    {
        System.Console.WriteLine(localTf);
        Debug.Log(localTf);
        Vector2 position = transform.position;
        position += localTf.x * (Vector2)transform.right;
        position += localTf.y * (Vector2)transform.up;
        return position;
    }

    public Vector2 WorldToLocal(Vector2 world)
    {
        Vector2 rel = world - (Vector2)transform.position;
        Debug.Log(rel);
        float x = Vector2.Dot(rel, (Vector2)transform.right);
        float y = Vector2.Dot(rel, (Vector2)transform.up);
        Debug.Log(new Vector2(x, y));
        return new Vector2(x, y);
    }
}
