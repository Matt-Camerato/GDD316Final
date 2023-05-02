using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float xRotSpeed = 250f;
    [SerializeField] private float yRotSpeed = 120f;
    [SerializeField] private float zoomInMin = 25;
    [SerializeField] private float zoomInMax = 55;
    
    private Vector3 offset;
    private float x;
    private float y;
    private Camera _camera;
    
    
    private void Start()
    {
        TryGetComponent(out _camera);
        //setup offset and angles
        offset = transform.position - target.position;
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        //set initial transform position and rotation
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        transform.position = target.position + rotation * offset;
        transform.LookAt(target.position);
    }

    private void LateUpdate()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            var camFOV = _camera.fieldOfView + -Input.mouseScrollDelta.y * 2;

            var clampedVal = Mathf.Clamp(camFOV, zoomInMin, zoomInMax);
            _camera.fieldOfView = clampedVal;
        }

        //rotate around target with right mouse button
        var rotation = Quaternion.Euler(y, x, 0);
        if (Input.GetMouseButton(1))
        {
            x += Input.GetAxis("Mouse X") * xRotSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * yRotSpeed * 0.02f;
            y = ClampAngle(y, -70, 70);
            rotation = Quaternion.Euler(y, x, 0);
            var position = target.position;
            transform.position = position + rotation * offset;
            transform.LookAt(position);
        }

        // Smoothly follow target
        var desiredPosition = target.position + rotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.1f);
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
