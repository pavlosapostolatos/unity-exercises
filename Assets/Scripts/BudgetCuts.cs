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

    private float freeSpaceLeft;
    private float freeSpaceRight;

    private float spaceLeft;
    private float spaceRight;

    private Item latestLeft;
    private Item latestRight;

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

        Item[] items = new Item[itemCount];
        for (int i = 0; i < itemCount; i++)
        {
            items[i] = new Item(default, i+1);
            // double angle = Math.Asin((double)items[i].Radius / arcRadius);
            double angle;
            double dir = 90 * Mathf.Deg2Rad;
            if (i == 0)
            {
                angle = Math.Acos((Math.Pow(arcRadius, 2f) + Math.Pow(arcRadius, 2f) - Math.Pow(items[i].Radius, 2f)) 
                                         / (2*arcRadius*arcRadius));
                angle = Math.Acos(1f - Math.Pow(items[i].Radius, 2f)
                    / (2*arcRadius*arcRadius));
                spaceRight += (float)angle;
                freeSpaceRight -= (float)angle;
                spaceLeft += (float)angle;
                freeSpaceLeft -= (float)angle;
                latestLeft = items[i];
                latestRight = items[i];
            }
            else if (freeSpaceLeft < freeSpaceRight)
            {
                //draw right
                angle = Math.Acos((Math.Pow(arcRadius, 2f) + Math.Pow(arcRadius, 2f) - Math.Pow(items[i].Radius + latestRight.Radius, 2f)) 
                                         / (2*arcRadius*arcRadius));
                angle = Math.Acos(1f - Math.Pow(items[i].Radius, 2f)
                                  / (2*arcRadius*arcRadius));
                double offset = spaceRight;
                dir -= (offset + angle);
                spaceRight += 2* (float)angle;
                freeSpaceRight -= 2*(float)angle;
                latestRight = items[i];
            }
            else
            {
                //draw left
                angle = Math.Acos((Math.Pow(arcRadius, 2f) + Math.Pow(arcRadius, 2f) - Math.Pow(items[i].Radius + latestLeft.Radius, 2f)) 
                                         / (2*arcRadius*arcRadius));
                angle = Math.Acos(1f - Math.Pow(items[i].Radius, 2f)
                    / (2*arcRadius*arcRadius));
                double offset = spaceLeft;
                dir += (offset + angle);
                spaceLeft += 2*(float)angle;
                freeSpaceLeft -= 2*(float)angle;
                latestLeft = items[i];
            }

            Vector2 posOnArc = AngToDir((float)dir) * arcRadius;
            Debug.Log(posOnArc);
            items[i].Position = posOnArc;
            Gizmos.DrawWireSphere(posOnArc, items[i].Radius);
        }
    }

    class Item
    {
        public Item(Vector2 position, float radius)
        {
            Position = position;
            Radius = radius;
        }

        public Vector2 Position { get; set; }

        public float Radius { get; set; }
    }
}