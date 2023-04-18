using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Pickup
{
    protected override void OnPickUp(FlingController playerController)
    {
        base.OnPickUp(playerController);
        playerController.ApplyForce(-Vector3.forward, 2, 2);
    }
}
