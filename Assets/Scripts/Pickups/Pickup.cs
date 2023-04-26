using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private protected const string Gravity = "Gravity";
    private protected const string Kinematics = "Kinematics";
    
    public virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.transform.root.TryGetComponent(out FlingController flingController);
        OnPickUp(flingController);
    }

    protected virtual void OnPickUp(FlingController playerController)
    {
        Destroy(gameObject);
    }
}
