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
            Ray ray = new Ray(LazerPosition, LazerDirection);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Gizmos.DrawLine(LazerPosition, hit.point);
                Gizmos.DrawSphere(hit.point, 0.1f);

                LazerDirection = Vector2.Reflect(LazerDirection, hit.normal);
                LazerPosition = hit.point;
            }
            else
            {
                break;
            }
        }

    }
}
