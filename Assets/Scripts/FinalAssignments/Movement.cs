using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 Acc => Physics.gravity;
    private  Vector3 velocity = Vector3.zero;

    void Start()
    {
        transform.position = Vector3.zero;
    }

    private void Awake()
    {
        
    }

    void Update()
    {
        Vector3 acceleration = Acc;
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            acceleration += 3 * Vector3.up - Acc;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            acceleration += 3 * Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            acceleration += 3 * Vector3.right;
        }
        if (Input.GetKey(KeyCode.B))
        {
            transform.position = Vector3.zero;
        }
        acceleration.Normalize();
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.Max(-5 * Vector3.one ,velocity);
        velocity = Vector3.Min(5 * Vector3.one ,velocity);
        if (velocity.y < -4)
            velocity.x *= 0.9f; // too much force downwards should reduce vertical velocity in my head lmao
        // comment out to have fulll freedom
        Vector3 displacement = velocity * Time.deltaTime;
        transform.position += displacement;
    }
}
