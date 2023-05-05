using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : EnemyController
{
    protected override void Awake()
    {
        TypeOfEnemy = global::TypeOfEnemy.EnemyType.Bomb;
        base.Awake();
    }

    protected override void AffectPlayer()
    {
        var go = Instantiate(throwable, throwablePos.position, Quaternion.identity);
        go.TryGetComponent(out Rigidbody rb);
        Throw(rb);
        base.AffectPlayer();
    }
}
