using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBomb : ThrowableCollider
{
    public float force;
    
    protected internal override void AffectPlayer(FlingController flingController)
    {
        flingController.ApplyForce(-flingController.rb.transform.forward, 2, force);
        base.AffectPlayer(flingController);
    }
}
