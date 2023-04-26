using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Pickup
{
    [SerializeField] private float force;
    
    protected override void OnPickUp(PickupManager playerController)
    {
        playerController.Force = force;
        playerController.ChangeCurrentPickup(PickupManager.CurrentPickup.Bomb);
        Destroy(gameObject);
        base.OnPickUp(playerController);
    }
}
