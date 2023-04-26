using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFling : Pickup
{
    [SerializeField] private int amountOfFlingsToAdd;
    
    protected override void OnPickUp(PickupManager playerController)
    {
        playerController.AmountOfFlings = amountOfFlingsToAdd;
        playerController.AddFlingPickup();
        base.OnPickUp(playerController);
    }
}
