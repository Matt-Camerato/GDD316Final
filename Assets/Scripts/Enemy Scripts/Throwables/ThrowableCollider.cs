using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableCollider : MonoBehaviour
{
    [SerializeField] public TypeOfEnemy.EnemyType TypeOfEnemy;

    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    protected internal virtual void AffectPlayer(FlingController flingController)
    {
        DestroyThis();
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3);
        DestroyThis();
    }
}
