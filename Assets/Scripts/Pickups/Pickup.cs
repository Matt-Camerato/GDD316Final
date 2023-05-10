using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private protected const string Gravity = "Gravity";
    private protected const string Kinematics = "Kinematics";
    private bool _collided;

    private void Start()
    {
        _collided = false;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(_collided) return;
        if (!other.CompareTag("Player")) return;
        _collided = true;
        other.transform.root.TryGetComponent(out PickupManager flingController);
        OnPickUp(flingController);
    }

    protected virtual void OnPickUp(PickupManager playerController)
    {
        AudioManager.Instance.CollectSFX();
        Destroy(gameObject);
    }
}
