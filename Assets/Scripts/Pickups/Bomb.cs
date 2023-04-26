using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Pickup
{
    [SerializeField] private float force;
    
    protected override void OnPickUp(FlingController playerController)
    {
        playerController.ApplyForce(playerController.transform.forward, 2, force);
        Destroy(gameObject);
        base.OnPickUp(playerController);
    }
}
