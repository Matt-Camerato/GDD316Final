using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFling : Pickup
{
    [SerializeField] private int amountOfFlingsToAdd;
    
    protected override void OnPickUp(FlingController playerController)
    {
        playerController.AddFlings(amountOfFlingsToAdd);
        base.OnPickUp(playerController);
    }
}
