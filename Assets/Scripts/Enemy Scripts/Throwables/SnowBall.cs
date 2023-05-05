using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : ThrowableCollider
{
    [HideInInspector] public float duration;
    
    protected internal override void AffectPlayer(FlingController flingController)
    {
        flingController.ChangeGravity("Kinematics", duration);
        Debug.Log("Snow Ball");
        base.AffectPlayer(flingController);
    }
}
