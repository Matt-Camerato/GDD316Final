using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEnemy : EnemyController
{
    [SerializeField] private GameObject rock;
    [SerializeField] private float force;
    
    protected override void AffectPlayer()
    {
        var go = Instantiate(rock);
        go.TryGetComponent(out Rigidbody rb);
        rb.AddForce(transform.forward * (force * Time.deltaTime));
        base.AffectPlayer();
    }
}
