using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowGravity : Pickup
{
    [SerializeField] private float duration;
    
    protected override void OnPickUp(PickupManager playerController)
    {
        playerController.Duration = duration;
        playerController.ChangeCurrentPickup(PickupManager.CurrentPickup.LowGravity);
        base.OnPickUp(playerController);
    }
}
