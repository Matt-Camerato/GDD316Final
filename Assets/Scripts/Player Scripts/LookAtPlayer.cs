using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform playerTransform;

    private void Start() => playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    private void Update()
    {
        transform.LookAt(playerTransform);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
