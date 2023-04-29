using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class TempLevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject level;

    private FlingController _flingController;

    private Transform _playerDistance;

    [SerializeField] private List<GameObject> _spawnedMeshes = new();

    private int _currentMesh = -1;

    [SerializeField] private float timeToSpawn;

    private int _amountSpawned;
    
    
    private void Start()
    {
        _amountSpawned = 0;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        _amountSpawned++;
        if (_amountSpawned % 5 == 0 && _amountSpawned != 0)
        {
            Destroy(_spawnedMeshes[0]);
            _spawnedMeshes.RemoveAt(0);
            _currentMesh--;
        }
        
        var spawnedMesh = Instantiate(level);
        spawnedMesh.transform.position = new Vector3(_currentMesh == -1 ? 0 : _spawnedMeshes[_currentMesh].transform.position.x + 41, 10, 0);
        _spawnedMeshes.Add(spawnedMesh);
        _currentMesh++;
        
        yield return new WaitForSeconds(timeToSpawn);
        StartCoroutine(Spawn());
    }

    private float PlayerDistance()
    {
        return Vector3.Distance(_playerDistance.position, _spawnedMeshes[_currentMesh + 1].transform.position);
    }

}
