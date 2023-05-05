using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableCollider : MonoBehaviour
{
    protected virtual void AffectPlayer()
    {
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
