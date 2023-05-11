using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChildCollisions : MonoBehaviour
{
    private static ChildCollisions _instance;
    
    private const string Walkable = "Walkable";
    private const string NotWalkable = "Not Walkable";
    private const string Throwable = "Throwables";
    private FlingController _flingController;

    private void Awake()
    {
        _instance = this;
        transform.root.TryGetComponent(out _flingController);
    }

    private void OnCollisionEnter(Collision other)
    {
        var go = other.gameObject;
        var layer = go.layer;
        if (layer == GetLayerName(Walkable) ||
            layer == GetLayerName(NotWalkable))
        {
            if (!_instance._flingController) return;
            _flingController.IsGrounded = true;
            transform.root.TryGetComponent(out EffectManager effectManager);
            effectManager.PlayEffect(EffectManager.Ground);
        }
        else if (layer == GetLayerName(Throwable))
        {
            _instance.ThrowableAttack(go);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        var layer = other.gameObject.layer;

        if (layer == GetLayerName("Walkable") ||
            layer == GetLayerName("Not Walkable")) return;
        if (!_flingController) return;
        _flingController.IsGrounded = false;
    }

    private void ThrowableAttack(GameObject collider)
    {
        collider.TryGetComponent(out ThrowableCollider throwableCollider);
        Debug.Log("Child Collision");

        throwableCollider.AffectPlayer(_flingController);
    }

    private static int GetLayerName(string layerName)
    {
        return LayerMask.NameToLayer(layerName);
    }
    
}
