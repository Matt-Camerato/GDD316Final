using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddFling : Pickup
{
    [SerializeField] private int amountOfFlingsToAdd;
    [SerializeField] private TMP_Text flingNumText;

    private void Start() => flingNumText.text = "+" + amountOfFlingsToAdd;
    
    protected override void OnPickUp(PickupManager playerController)
    {
        playerController.AmountOfFlings = amountOfFlingsToAdd;
        playerController.AddFlingPickup();
        base.OnPickUp(playerController);
    }
}
