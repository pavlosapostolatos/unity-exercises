using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingLazer : MonoBehaviour
{
    
    public int maxBounces = 5;
    
    
    private void OnDrawGizmos()
    {
        Vector2 LazerDirection = transform.right;
        Vector2 LazerPosition = transform.position;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(LazerPosition, LazerPosition + LazerDirection);

        for (int i = 0; i < maxBounces; i++)
        {
            Gizmos.color = Color.red;
            Ray ray = new Ray(LazerPosition, LazerDirection);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Gizmos.DrawLine(LazerPosition + LazerDirection, hit.point);
                Gizmos.DrawSphere(hit.point, 0.1f);
                Debug.Log(LazerDirection);

                LazerDirection = myReflect(LazerDirection, hit.normal);
                LazerPosition = hit.point;
                Gizmos.color = Color.green;
                Gizmos.DrawLine(hit.point,hit.point + hit.normal);
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(hit.point, (Vector2)hit.point + LazerDirection);
                Debug.Log(hit.normal);
                Debug.Log(LazerDirection);
            }
            else
            {
                break;
            }
        }
    }
    
    private Vector2 myReflect(Vector2 direction,Vector2 normal)
    {
        Vector2 VectorProjection_withOppositeDirection = normal.normalized * Vector2.Dot(-direction,normal.normalized); //name is shitty because that's how i drew it lmao
        return direction + 2 * VectorProjection_withOppositeDirection;
    }
}
