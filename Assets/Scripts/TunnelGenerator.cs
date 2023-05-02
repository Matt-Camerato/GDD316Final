using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TunnelGenerator : MonoBehaviour
{
    public static TunnelGenerator Instance;
    public static event Action GeneratedPiece;
    [Header("Tunnel Settings")]
    [SerializeField] private int renderDistance = 6;
    [SerializeField] private int powerupFrequency = 3;

    [Header("References")]
    [SerializeField] private GameObject tunnelStart;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform tunnelPieceParent;
    [SerializeField] private List<GameObject> tunnelPieces = new List<GameObject>();

    private List<GameObject> currentPieces = new List<GameObject>();

    private int lastPieceAdded = 0;

    private void Awake() => Instance = this;

    private void Start()
    {
        //spawn pieces in front of player within their render distance
        for(int i = 1; i <= renderDistance; i++) SpawnPiece(i);
    }
    
    public void UpdatePieces(int pieceReached)
    {
        //hide any old pieces outside of player's render distance
        if(pieceReached == renderDistance) tunnelStart.SetActive(false);
        else if(pieceReached > renderDistance)
        {
            int pieceToDelete = pieceReached - renderDistance;
            currentPieces[pieceToDelete].SetActive(false);
        }

        //spawn new piece within the player's render distance
        SpawnPiece(pieceReached + renderDistance);
    }

    private void SpawnPiece(int index)
    {
        //get prefab to spawn
        GeneratedPiece?.Invoke();
        int prefabIndex = Random.Range(0, tunnelPieces.Count);
        while(lastPieceAdded == prefabIndex) prefabIndex = Random.Range(0, tunnelPieces.Count);
        lastPieceAdded = prefabIndex;
        GameObject prefab = tunnelPieces[prefabIndex];
        
        //spawn piece with x offset
        Vector3 xOffset = Vector3.right * 40 * index;
        Vector3 spawnPos = transform.position + xOffset;
        GameObject newPiece = Instantiate(prefab, spawnPos, Quaternion.identity, tunnelPieceParent);
        currentPieces.Add(newPiece);
        newPiece.GetComponent<TunnelPiece>().index = index;
        
        //spawn with just flings or powerups depending on piece index
        if(index % powerupFrequency == 0) newPiece.GetComponent<TunnelPiece>().SpawnWithPowerup();
        else newPiece.GetComponent<TunnelPiece>().SpawnWithoutPowerup();
    }
}
