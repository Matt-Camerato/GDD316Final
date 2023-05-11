using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshSurface))]
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
        //decide whether to spawn one or both pickups
        //every 10 tunnel pieces the chance to spawn 1 pickup increases by 5% (starts at 10%, max is 70%)
        float chanceToSpawn1 = Mathf.Min(0.1f + (0.5f * Mathf.Ceil(index / 10)), 0.7f);
        if(Random.value < chanceToSpawn1)
        {
            //spawn one pickup
            int flings = GetRandomNumFlings();
            CreateFlingPickup(pickupSpots[0].position, flings);
        }
        else
        {
            //spawn both pickups
            for(int i = 0; i < pickupSpots.Count; i++)
            {
                int flings = GetRandomNumFlings();
                CreateFlingPickup(pickupSpots[i].position, flings);
            }
        }
    }

    public void SpawnWithPowerup()
    {
        for(int i = 0; i < pickupSpots.Count; i++)
        {
            if(i != pickupSpots.Count - 1)
            {
                int flings = GetRandomNumFlings();
                CreateFlingPickup(pickupSpots[i].position, flings);
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

    private int GetRandomNumFlings()
    {
        int min = Mathf.Max(Mathf.FloorToInt(4 - (0.5f * Mathf.Floor(index / 5))), 1);
        int max = Mathf.Max(Mathf.CeilToInt(5 - (0.5f * Mathf.Floor(index / 5))), 3);

        int numFlings = Random.Range(min, max + 1);
        return numFlings;
    }

    private void CreateFlingPickup(Vector3 pos, int numFlings)
    {
        GameObject fling = Instantiate(flingPickup, pos, Quaternion.identity, transform);
        AddFling addFling = fling.GetComponent<AddFling>();
        addFling.SetFlings(numFlings);
        pickups.Add(fling);
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
