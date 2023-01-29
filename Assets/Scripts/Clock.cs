using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private float TAU = Mathf.PI * 2;
    
    public int clockSize = 5;

    public Vector2 AngToDir(float dir) => new Vector2(Mathf.Cos(dir), Mathf.Sin(dir));

    public void OnDrawGizmos()
    {
        Handles.matrix = Gizmos.matrix = transform.localToWorldMatrix;

        Handles.DrawWireDisc(Vector3.zero, Vector3.forward, clockSize);
        DateTime now = DateTime.Now;
        int hour = now.Hour;
        int minute = now.Minute;
        int second = now.Second;

        Gizmos.color = Color.red;
        float hourAngle = 90f * Mathf.Deg2Rad - TAU / 24f * hour; //offset by 90 degrees to make 12 o'clock the top and split TAU between 24 hours
        Vector2 hourPos = AngToDir(hourAngle);
        Gizmos.DrawLine(Vector3.zero, (Vector3)hourPos * clockSize);

        Gizmos.color = Color.green;
        float minuteAngle = 90f * Mathf.Deg2Rad - TAU / 60f * minute;
        Vector2 minutePos = AngToDir(minuteAngle);
        Gizmos.DrawLine(Vector3.zero, (Vector3)minutePos * clockSize);

        Gizmos.color = Color.blue;
        float secondAngle = 90f * Mathf.Deg2Rad - TAU / 60f * second;
        Vector2 secondPos = AngToDir(secondAngle);
        Gizmos.DrawLine(Vector3.zero, (Vector3)secondPos * clockSize);

        Handles.color = Color.black;
        for( int i = 0; i < 60; i++ ) {
            secondAngle = 90f * Mathf.Deg2Rad - TAU / 60f * i;
            secondPos = AngToDir(secondAngle);
            int thickness = 1 + 3 * Convert.ToInt32(i % 5 == 0) + 5 * Convert.ToInt32(i % 15 == 0) ;
            Handles.DrawLine( secondPos * clockSize, secondPos * ( 1f - 0.1f ) * clockSize, thickness );
        }
    }
}