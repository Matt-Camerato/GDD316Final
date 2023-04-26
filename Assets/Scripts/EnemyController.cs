using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private protected FlingController PlayerController;
    private protected const string Gravity = "Gravity";
    private protected const string Kinematics = "Kinematics";
    private Transform _playerTransform;
    [SerializeField] private bool _affectedPlayer;
    
    private float _distance;

    [SerializeField] protected float minDistance;
    
    protected virtual void Start()
    {
        _affectedPlayer = false;
        GameObject.FindGameObjectWithTag($"Player").transform.root.TryGetComponent(out PlayerController);
        _playerTransform = PlayerController.rb.transform;
    }

    protected virtual void Update()
    {
        _distance = Vector3.Distance(transform.position, _playerTransform.position);
        Debug.Log(_distance);
        if(!PlayerInRange()) return;
        CheckForPlayer();
    }

    private bool PlayerInRange()
    {
        return _distance < minDistance;
    }

    private void CheckForPlayer()
    {
        transform.LookAt(_playerTransform);

        // Get the position of the raycast origin
        var originPos = transform.position;

        // Create a raycast from the position of the raycast origin
        var ray = new Ray(originPos, transform.forward);

        // Create a RaycastHit variable to store information about the hit object
        RaycastHit hit;

        // Cast the ray and check if it hit something
        if (Physics.Raycast(ray, out hit))
        {
            // Draw a line from the raycast origin to the hit point
            Debug.DrawLine(originPos, hit.point, Color.green);
            if (_affectedPlayer) return;
            StartCoroutine(WaitToAffect());
        }
        else
        {
            // Draw a line from the raycast origin out to a maximum distance
            Debug.DrawLine(originPos, originPos + ray.direction * 100, Color.red);
        }
    }

    private IEnumerator WaitToAffect()
    {
        _affectedPlayer = true;
        AffectPlayer();
        yield return new WaitForSeconds(7);
        yield return new WaitUntil(PlayerController.CanBeTargeted);
        _affectedPlayer = false;
    }
    
    protected virtual void AffectPlayer()
    {
       // Debug.Log("Sees Player");
    }
}
