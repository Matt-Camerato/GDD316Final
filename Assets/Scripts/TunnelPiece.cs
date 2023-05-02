using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]
public class TunnelPiece : MonoBehaviour
{
    public int index;

    [SerializeField] private GameObject flingPickup;
    [SerializeField] private List<GameObject> powerups = new List<GameObject>();
    [SerializeField] private List<Transform> pickupSpots = new List<Transform>();

    private List<GameObject> pickups = new List<GameObject>();

    private bool reached = false;

    public void SpawnWithoutPowerup()
    {
        foreach(Transform spot in pickupSpots)
        {
            GameObject fling = Instantiate(flingPickup, spot.position, Quaternion.identity, transform);
            pickups.Add(fling);
        }
    }

    public void SpawnWithPowerup()
    {
        for(int i = 0; i < pickupSpots.Count; i++)
        {
            if(i != pickupSpots.Count - 1)
            {
                GameObject fling = Instantiate(flingPickup, pickupSpots[i].position, Quaternion.identity, transform);
                pickups.Add(fling);
            }
            else
            {
                //spawn powerup on last pickup spot
                int index = Random.Range(0, powerups.Count);
                GameObject powerup = Instantiate(powerups[index], pickupSpots[i].position, Quaternion.identity, transform);
                pickups.Add(powerup);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !reached) 
        {
            TunnelGenerator.Instance.UpdatePieces(index);
            reached = true;
        }
    }

}
