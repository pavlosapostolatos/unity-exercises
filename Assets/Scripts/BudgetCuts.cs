using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BudgetCuts : MonoBehaviour
{
    public float arcRadius = 3;
    public float arcAngle = 60;
    public int itemCount = 3;
    
    public Item[] items;

    private float freeSpaceLeft;
    private float freeSpaceRight;

    private float spaceLeft;
    private float spaceRight;

    public Vector2 AngToDir(float dir) => new Vector2(Mathf.Cos(dir), Mathf.Sin(dir));

    private void OnDrawGizmos()
    {
        freeSpaceLeft = arcAngle;
        freeSpaceRight = arcAngle;
        spaceLeft = 0;
        spaceRight = 0;

        Gizmos.matrix = transform.localToWorldMatrix;
        Handles.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(default, Vector3.up * arcRadius);
        // Gizmos.DrawWireSphere(default, arcRadius);
        Handles.DrawWireArc(default, Vector3.forward, Vector3.up, arcAngle, arcRadius);
        Handles.DrawWireArc(default, Vector3.forward, Vector3.up, -arcAngle, arcRadius);

        float arcCircumference = 2 * arcRadius * Mathf.PI * arcAngle / 360;

        // Item[] items = new Item[itemCount];
        for (int i = 0; i < itemCount; i++)
        {
            // items[i] = new Item(default, i+1);
            double angle = Math.Asin((double)items[i].radius / arcRadius);
            double dir = 90 * Mathf.Deg2Rad;
            if (i == 0)
            {
                spaceRight += (float)angle;
                freeSpaceRight -= (float)angle;
                spaceLeft += (float)angle;
                freeSpaceLeft -= (float)angle;
            }
            else if (freeSpaceLeft < freeSpaceRight)
            {
                //draw right
                double offset = spaceRight;
                dir -= (offset + angle);
                spaceRight += 2* (float)angle;
                freeSpaceRight -= 2*(float)angle;
            }
            else
            {
                //draw left
                double offset = spaceLeft;
                dir += (offset + angle);
                spaceLeft += 2*(float)angle;
                freeSpaceLeft -= 2*(float)angle;
            }

            Vector2 posOnArc = AngToDir((float)dir) * arcRadius;
            Debug.Log(posOnArc);
            items[i].transform.position = posOnArc;
            items[i].transform.rotation = Quaternion.Euler(0, 0, (float)dir * Mathf.Rad2Deg - 90);
            Gizmos.DrawWireSphere(posOnArc, items[i].radius);
        }
    }

    [Serializable]
    public class Item
    {

        public Transform transform;
        public float radius;
        
        public Item(Transform transform, float radius)
        {
            this.transform = transform;
            this.radius = radius;
        }
    }
}