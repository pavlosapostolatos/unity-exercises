using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor;

public class RadialTrigger : MonoBehaviour
{
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public Transform pointTf;

    [Range(0f, 4f)]
    public float radius = 1f;

    void OnDrawGizmos() {
        Vector2 objPos = pointTf.position;
        Vector2 origin = transform.position;
        float dist = Vector2.Distance(objPos, origin);
        bool inInside = dist < radius;
        Gizmos.color = inInside ? Color.green : Color.red;
        Gizmos.DrawWireSphere(origin, radius);
    }



}
