using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableCollider : MonoBehaviour
{
    [SerializeField] public TypeOfEnemy.EnemyType TypeOfEnemy;
    [SerializeField] private ParticleSystem particleEffect;
    
    private void Start()
    {
        StartCoroutine(DestroyAfterTime(5));
    }

    protected internal virtual void AffectPlayer(FlingController flingController)
    {
        if(particleEffect) particleEffect.Play();
        var duration = particleEffect ? ParticleDuration() : 0;
        StartCoroutine(DestroyAfterTime(duration));
    }

    private float ParticleDuration()
    {
        var main = particleEffect.main;
        return main.duration;
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyThis();
    }
}
