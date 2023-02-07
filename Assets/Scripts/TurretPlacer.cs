using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacer : MonoBehaviour
{
    public Transform turret;

    public bool invertYAxis = false;
    public int mouseSensitivity = 1;
    public int turretYawSensitivity = 1;

    float pitchDeg;
    float yawDeg;
    float turretYawOnScroll;

    void Awake()
    {
        Vector3 startEuler = transform.eulerAngles;
        pitchDeg = startEuler.x;
        yawDeg = startEuler.y;
        transform.rotation = Quaternion.Euler(pitchDeg, yawDeg, 0);
    }

    void Update()
    {
        UpdateTurretYawInput();
        UpdateMouseLook();
        
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            turret.position = hit.point;
            Vector3 yAxis = hit.normal;
            Vector3 zAxis = Vector3.Cross(transform.right, yAxis).normalized;
            Debug.DrawLine(ray.origin, hit.point);
            Quaternion offsetRot = Quaternion.Euler( 0, turretYawOnScroll, 0 );
            turret.rotation = Quaternion.LookRotation(zAxis, yAxis) * offsetRot;
        }
    }

    void UpdateMouseLook()
    {
        float xDelta = Input.GetAxis("Mouse X");
        float yDelta = Input.GetAxis("Mouse Y");
        Debug.Log($"xDelta={xDelta}, yDelta={yDelta}");
        pitchDeg += (invertYAxis ? yDelta : -yDelta) * mouseSensitivity;
        pitchDeg = Mathf.Clamp(pitchDeg, -90, 90);
        yawDeg += xDelta * mouseSensitivity;
        transform.rotation = Quaternion.Euler(pitchDeg, yawDeg, 0);//the turret can't rotate on it's back. only shoots 180 deg on front
    }
    
    void UpdateTurretYawInput() {
        float scrollDelta = Input.mouseScrollDelta.y;
        turretYawOnScroll += scrollDelta * turretYawSensitivity;
        turretYawOnScroll = Mathf.Clamp( turretYawOnScroll, -90, 90 );
    }
}