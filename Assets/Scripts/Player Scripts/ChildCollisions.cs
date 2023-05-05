using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollisions : MonoBehaviour
{
    private const string Walkable = "Walkable";
    private const string NotWalkable = "Not Walkable";
    private const string Throwable = "Throwables";
    
    private void OnCollisionEnter(Collision other)
    {
        var layer = other.gameObject.layer;

        if (layer != GetLayerName(Walkable) &&
            layer != GetLayerName(NotWalkable))
        {
            transform.root.TryGetComponent(out FlingController flingController);
            if (!flingController) return;
            flingController.IsGrounded = true;
        }
        else if (layer == GetLayerName(Throwable))
        {
            ThrowableAttack();
        }
    } 
    
    private void OnCollisionExit(Collision other)
    {
        var layer = other.gameObject.layer;

        if (layer == GetLayerName("Walkable") ||
            layer == GetLayerName("Not Walkable")) return;
        transform.root.TryGetComponent(out FlingController flingController);
        if (!flingController) return;
        flingController.IsGrounded = false;
    }

    private void ThrowableAttack()
    {
        
    }

    private static int GetLayerName(string layerName)
    {
        return LayerMask.NameToLayer(layerName);
    }
}
