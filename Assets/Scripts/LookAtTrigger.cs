using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using UnityEditor;

public class LookAtTrigger : MonoBehaviour
{
    public Transform target;
    public float dotProductResult;
    [Range(0f, 1f)] public float Treshold = 0f;

    void OnDrawGizmos()
    {
        Vector2 currentPos = this.transform.position;
        Vector2 targetPos = this.target.position;
        Vector2 targetToCurrent = currentPos - targetPos;
        Vector2 targetPosLookingAt = this.target.right;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(targetPos,targetPos + targetPosLookingAt);

        // Debug.Log(targetPos);
        dotProductResult = Vector2.Dot(targetToCurrent.normalized, targetPosLookingAt.normalized);
        if (dotProductResult >= this.Treshold)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawLine(this.transform.position, this.target.position);
        Gizmos.DrawLine(Vector2.zero, targetPos.normalized * dotProductResult);
        Gizmos.DrawWireSphere(Vector2.zero, 1.0f);
    }
}