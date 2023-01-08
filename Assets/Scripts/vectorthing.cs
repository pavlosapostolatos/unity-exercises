using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vectorthing : MonoBehaviour
{
    public Transform aTf;
    public Transform bTf;
    void OnDrawGizmos() {
        Vector2 pt = transform.position;
        float length = pt.magnitude;
        Vector2 dirToPt = pt.normalized;
        Gizmos.DrawLine(Vector2.zero, dirToPt);
        Gizmos.DrawLine(aTf.position, bTf.position);
        Gizmos.DrawLine(Vector2.zero, bTf.position);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)==true){
           Debug.Log("space was pressed"); 
        //    GetComponent<Transform>().position
            Vector2 movement = new Vector2(1, 1);
            transform.Translate(movement);
        }
    }                     
}
