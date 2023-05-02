using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Walkable") &&
            other.gameObject.layer != LayerMask.NameToLayer("Not Walkable")) return;
        transform.root.TryGetComponent(out FlingController flingController);
        if(!flingController) return;
        flingController.IsGrounded = true;
    } 
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Walkable") &&
            other.gameObject.layer != LayerMask.NameToLayer("Not Walkable")) return;
        transform.root.TryGetComponent(out FlingController flingController);
        if(!flingController) return;
        flingController.IsGrounded = false;
    }
}
