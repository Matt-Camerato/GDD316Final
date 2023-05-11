using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddFling : Pickup
{
    [SerializeField] private int amountOfFlingsToAdd;
    [SerializeField] private TMP_Text flingNumText;

    private void Start() => flingNumText.text = "+" + amountOfFlingsToAdd;

    public void SetFlings(int flings)
    {
        amountOfFlingsToAdd = flings;
        flingNumText.text = "+" + flings;
    }
    
    protected override void OnPickUp(PickupManager playerController)
    {
        playerController.AddFlingPickup(amountOfFlingsToAdd);
        base.OnPickUp(playerController);
    }
}
