using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FlingController : MonoBehaviour
{
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float forceMultiplier = 10f;
    [SerializeField] private float maxForce = 50f;
    [SerializeField] private float stopVelocity = 0.01f;
    [SerializeField] private int segments = 20;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Rigidbody rb;

    private Rigidbody[] _rigidbodies;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private bool _isDragging = false;
    private bool _isMoving = false;

    [SerializeField] private int totalAmountOfFlings;

    private int _currentFlings;

    private void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _currentFlings = 0;
    }

    private void Update()
    {
        //stop player completely once they are slower than the stop velocity
        if (rb.velocity.magnitude < stopVelocity) StopMoving();

        if (_isMoving) return; //don't allow another fling while player is moving
        if (_currentFlings >= totalAmountOfFlings) return;

        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = rb.transform.position;
            _isDragging = true;
        }

        if (_isDragging)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _endPosition = Physics.Raycast(ray, out var hit, maxDistance) ? hit.point : ray.GetPoint(maxDistance);

            //set the positions of the line renderer to form an arc
            var arcPoints = new Vector3[segments + 1];
            for (var i = 0; i <= segments; i++)
            {
                var t = (float)i / (float)segments;
                arcPoints[i] = Vector3.Lerp(_startPosition, _endPosition, t) + CalculateArcPoint(t);
            }

            lineRenderer.positionCount = arcPoints.Length;
            lineRenderer.SetPositions(arcPoints);
        }

        if (!Input.GetMouseButtonUp(0)) return;
        _isDragging = false;
        _isMoving = true;

        //calculate direction and distance of fling
        var forceDirection = _endPosition - _startPosition;
        var distance = forceDirection.magnitude;
        forceDirection.Normalize();

        //apply force to rigidbody
        ApplyForce(forceDirection, distance, forceMultiplier);


        //clear line renderer
        lineRenderer.positionCount = 0;
    }

    private void StopMoving()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        _isMoving = false;
    }

    private Vector3 CalculateArcPoint(float t)
    {
        //calculate height of arc based on the distance between the start and end points
        var arcHeight = Vector3.Distance(_startPosition, _endPosition) / 2f;

        //calculate the position of the arc point using a quadratic equation
        var result = Vector3.zero;
        result.y = arcHeight * 4f * t * (1f - t);
        result.x = arcHeight * (_endPosition.x - _startPosition.x) * t;
        result.z = arcHeight * (_endPosition.z - _startPosition.z) * t;
        return result;
    }

    public void ApplyForce(Vector3 forceDirection, float distance, float forceMultiplier)
    {
        //apply force to rigidbody
        _currentFlings++;
        var force = forceDirection * (distance * forceMultiplier);
        if (force.magnitude > maxForce)
        {
            force = force.normalized * maxForce;
        }

        //Debug.Log(force.magnitude);
        rb.AddForce(force, ForceMode.Impulse);
    }

    public void ChangeGravity()
    {
        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.useGravity = false;
        }
    }

    public void AddFlings(int flings)
    {
        totalAmountOfFlings += flings;
    }
}