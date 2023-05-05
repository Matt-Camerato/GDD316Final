using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private protected FlingController PlayerController;
    [SerializeField] private protected TypeOfEnemy.EnemyType TypeOfEnemy;
    [SerializeField] protected GameObject throwable;
    [SerializeField] protected Transform throwablePos;
    
    private protected const string Gravity = "Gravity";
    private protected const string Kinematics = "Kinematics";
    private Transform _playerTransform;
    [SerializeField] private bool _affectedPlayer;
    [HideInInspector] public NavMeshAgent agent;
    private float _distance;
    private float walkRadius = 5;
    
    [SerializeField] protected float minDistance;
    [SerializeField] protected float throwForce;

    [SerializeField]private bool canAttack;
    
    protected virtual void Awake()
    {
        TryGetComponent(out agent);
        _affectedPlayer = false;
        GameObject.FindGameObjectWithTag($"Player").transform.root.TryGetComponent(out PlayerController);
        _playerTransform = PlayerController.rb.transform;
        canAttack = true;
        if (PlayerPrefs.GetInt("FirstSpawn") != 1) return;
        canAttack= false;
        StartCoroutine(InitialWaitToAttack());
    }

    private IEnumerator InitialWaitToAttack()
    {
        yield return new WaitForSeconds(4);
        canAttack = true;
    }
    
    protected virtual void Update()
    {
        var dist = agent.remainingDistance; 
        if (!float.IsPositiveInfinity(dist) && agent.pathStatus== NavMeshPathStatus.PathComplete && agent.remainingDistance==0)//Arrived.
        {
            Wonder();
        }
        
        _distance = Vector3.Distance(transform.position, _playerTransform.position);
        if(!PlayerInRange()) return;
        CheckForPlayer();
    }
    
    private void Wonder()
    {
        var randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMesh.SamplePosition(randomDirection, out var hit, walkRadius, 1);
        var finalPosition = hit.position;
        agent.destination = finalPosition;
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

        // Cast the ray and check if it hit something
        if (Physics.Raycast(ray, out var hit))
        {
            // Draw a line from the raycast origin to the hit point
            Debug.DrawLine(originPos, hit.point, Color.green);
            if(!canAttack) return;
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
        if(PlayerController.IsGrounded) yield break;
        canAttack = false;
        AffectPlayer();
        yield return new WaitForSeconds(7);
        canAttack = true;
    }

    protected void Throw(Rigidbody rb)
    {
        rb.AddForce(throwablePos.forward * (throwForce * 150), ForceMode.Force);
    }
    
    protected virtual void AffectPlayer()
    {
        StartCoroutine(PlayerController.GotAttacked());
    }
}
