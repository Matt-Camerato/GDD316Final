using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : ThrowableCollider
{
    protected internal override void AffectPlayer(FlingController flingController)
    {
        AudioManager.Instance.HitSFX();
        base.AffectPlayer(flingController);
    }
}
