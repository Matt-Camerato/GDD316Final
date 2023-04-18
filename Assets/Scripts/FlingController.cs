using UnityEngine;

public class FlingController : MonoBehaviour
{
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float forceMultiplier = 10f;
    [SerializeField] private float maxForce = 50f;
    [SerializeField] private float stopVelocity = 0.01f;
    [SerializeField] private int segments = 20;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Rigidbody rb;
    
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool isDragging = false;
    private bool isMoving = false;

    private void Update()
    {
        //stop player completely once they are slower than the stop velocity
        if(rb.velocity.magnitude < stopVelocity) StopMoving();

        if(isMoving) return; //don't allow another fling while player is moving

        if (Input.GetMouseButtonDown(0))
        {
            startPosition = rb.transform.position;
            isDragging = true;
        }

        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                endPosition = hit.point;
            }
            else endPosition = ray.GetPoint(maxDistance);

            //set the positions of the line renderer to form an arc
            Vector3[] arcPoints = new Vector3[segments + 1];
            for (int i = 0; i <= segments; i++)
            {
                float t = (float)i / (float)segments;
                arcPoints[i] = Vector3.Lerp(startPosition, endPosition, t) + CalculateArcPoint(t);
            }
            lineRenderer.positionCount = arcPoints.Length;
            lineRenderer.SetPositions(arcPoints);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            isMoving = true;

            //calculate direction and distance of fling
            Vector3 forceDirection = (endPosition - startPosition);
            float distance = forceDirection.magnitude;
            forceDirection.Normalize();

            //apply force to rigidbody
            Vector3 force = forceDirection * distance * forceMultiplier;
            if(force.magnitude > maxForce) force = force.normalized * maxForce;
            Debug.Log(force.magnitude);
            rb.AddForce(force, ForceMode.Impulse);

            //clear line renderer
            lineRenderer.positionCount = 0;
        }
    }

    private void StopMoving()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isMoving = false;
    }

    private Vector3 CalculateArcPoint(float t)
    {
        //calculate height of arc based on the distance between the start and end points
        float arcHeight = Vector3.Distance(startPosition, endPosition) / 2f;
 
        //calculate the position of the arc point using a quadratic equation
        Vector3 result = Vector3.zero;
        result.y = arcHeight * 4f * t * (1f - t);
        result.x = arcHeight * (endPosition.x - startPosition.x) * t;
        result.z = arcHeight * (endPosition.z - startPosition.z) * t;
        return result;
    }
}