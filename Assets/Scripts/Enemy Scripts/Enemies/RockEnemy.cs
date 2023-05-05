using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEnemy : EnemyController
{
    protected override void Awake()
    {
        TypeOfEnemy = global::TypeOfEnemy.EnemyType.Rock;
        base.Awake();
    }
    
    protected override void AffectPlayer()
    {
        var go = Instantiate(throwable, throwablePos.position, Quaternion.identity);
        go.TryGetComponent(out Rigidbody rb);
        rb.mass *= 10;
        throwForce *= 10;
        Throw(rb);
        base.AffectPlayer();
    }
}
