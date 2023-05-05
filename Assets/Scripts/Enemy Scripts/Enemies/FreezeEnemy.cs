using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEnemy : EnemyController
{
    [SerializeField] private float duration;
    
    protected override void Awake()
    {
        TypeOfEnemy = global::TypeOfEnemy.EnemyType.Freeze;
        base.Awake();
    }
    
    protected override void AffectPlayer()
    {
        var go = Instantiate(throwable, throwablePos.position, Quaternion.identity);
        go.TryGetComponent(out SnowBall snowBall);
        go.TryGetComponent(out Rigidbody rb);
        snowBall.duration = duration;
        snowBall.TypeOfEnemy = TypeOfEnemy;
        Throw(rb);
        base.AffectPlayer();
    }
}
