using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private TypeOfEnemy _enemies;

    [SerializeField] [Range(4, 20)] private float timeToSpawn;
    [SerializeField] private int amountOfEnemiesToSpawn;

    private static bool _isSpawning;
    private float _randomTime;
    private int _amountOfEnemiesSpawned;

    
    private void OnEnable()
    {
        _enemies = Resources.Load<TypeOfEnemy>("Scriptable Objects/Enemies");
        _isSpawning = false;
        _randomTime = RandomizeFloat(4, timeToSpawn);
        TunnelGenerator.GeneratedPiece += SpawnManager;
    }

    private void OnDisable()
    {
        TunnelGenerator.GeneratedPiece -= SpawnManager;
    }

    private void SpawnManager()
    {
        var randomNum = RandomizeInt(0, _enemies.enemies.Length);
        Debug.Log(randomNum);
        RandomEnemy = _enemies.enemies[randomNum];
        StartCoroutine(Spawn());
    }

    private GameObject RandomEnemy
    {
        get;
        set;
    }

    private IEnumerator Spawn()
    {
        while (_amountOfEnemiesSpawned <= amountOfEnemiesToSpawn)
        {
            if (_isSpawning) yield break;
            _isSpawning = true;
            var enemy = Instantiate(RandomEnemy);
            enemy.TryGetComponent(out EnemyController enemyController);
            enemyController.agent.enabled = false;
            enemyController.agent.Warp(GetRandomPoint());
            enemyController.agent.enabled = true;
            _amountOfEnemiesSpawned++;
            yield return new WaitForSeconds(_randomTime);
            _randomTime = RandomizeFloat(4, timeToSpawn);
            _isSpawning = false;
            yield return null;
        }

        _amountOfEnemiesSpawned = 0;

        //StartCoroutine(Spawn());
    }

    private float RandomizeFloat(float min, float max)
    {
        return Random.Range(min, max);
    }
    
    private int RandomizeInt(int min, int max)
    {
        return Random.Range(min, max);
    }
    
    // Get Random Point on a Navmesh surface
    private Vector3 GetRandomPoint()
    {
        var navMeshData = NavMesh.CalculateTriangulation();
 
        // Pick the first index of a random triangle in the nav mesh
        var t = Random.Range(0, navMeshData.indices.Length-3);
         
        // Select a random point on it
        var point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t+1]], Random.value);
        Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t+2]], Random.value);
 
        return point;
    }
}
