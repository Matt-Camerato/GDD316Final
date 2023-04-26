using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowGravity : Pickup
{
    protected override void OnPickUp(FlingController playerController)
    {
        playerController.ChangeGravity(Gravity);
        base.OnPickUp(playerController);
    }
}
