using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEnemy : EnemyController
{
    [SerializeField] private float duration;
    protected override void AffectPlayer()
    {
        base.AffectPlayer();
        PlayerController.ChangeGravity("Kinematics", duration);
    }
}
