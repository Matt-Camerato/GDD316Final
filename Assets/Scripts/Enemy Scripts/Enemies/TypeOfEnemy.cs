using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TypeOfEnemy : ScriptableObject
{
    public GameObject[] enemies;

    public enum EnemyType
    {
        Rock,
        Bomb,
        Freeze
    }

    public EnemyType enemyType;
}
