using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor;

public class LookAtTrigger : MonoBehaviour
{
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public Transform target;
    public float dotProductResult;
    [Range(0f, 1f)]
    public float Treshold = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDrawGizmos()
    {
        Vector2 currentPos = this.transform.position;
        Vector2 targetPos = this.target.position;

        Debug.Log(targetPos);
        dotProductResult = Vector2.Dot(targetPos.normalized, currentPos.normalized);
        if (System.Math.Abs(dotProductResult) > this.Treshold)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawLine(this.transform.position, this.target.position);
        Gizmos.DrawLine(Vector2.zero, targetPos.normalized  * dotProductResult);
        Gizmos.DrawWireSphere(Vector2.zero, 1.0f);
    }
    
}
