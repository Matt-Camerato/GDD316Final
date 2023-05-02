using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private TypeOfEnemy _enemies;
    private GameObject _enemy;
    
    [SerializeField] [Range(4, 20)]private float timeToSpawn;
    private static bool _isSpawning;
    [SerializeField] private NavMeshData _navMeshData;
    private float _randomTime;
    
    private void Start()
    {
        _enemies = Resources.Load<TypeOfEnemy>("Scriptable Objects/Enemies");
        _enemy = _enemies.enemy[(int)Randomize(0, _enemies.enemy.Length)];
        _isSpawning = false;
        _randomTime = Randomize(4, timeToSpawn);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        if (_isSpawning) yield break;
        _isSpawning = true;
        var enemy = Instantiate(_enemy);
        enemy.TryGetComponent(out EnemyController enemyController);
        enemyController.agent.enabled = false;
        enemyController.agent.Warp(GetRandomPoint());
        enemyController.agent.enabled = true;
        yield return new WaitForSeconds(_randomTime);
        _randomTime = Randomize(4, timeToSpawn);
        _isSpawning = false;
        //StartCoroutine(Spawn());
    }

    private float Randomize(float min, float max)
    {
        return Random.Range(min, max);
    }
    
    // Get Random Point on a Navmesh surface
    private Vector3 GetRandomPoint()
    {
        var navMeshData = NavMesh.CalculateTriangulation();
 
        // Pick the first indice of a random triangle in the nav mesh
        var t = Random.Range(0, navMeshData.indices.Length-3);
         
        // Select a random point on it
        var point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t+1]], Random.value);
        Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t+2]], Random.value);
 
        return point;
    }
}
