using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    [CreateAssetMenu(fileName = "EnemyManagerData", menuName = "EnemySpawner/EnemyManagerData", order = 1)]
    public class EnemyManagerData : ScriptableObject
    {
        public float StartDelayTime;
        public float DelayHordes;

        public int MaxEnemiesInScene;
        public List<int> IncreaseQuantity;
        public int AfterHowSpawnIncrease;

        [Header("Datas")]
        public List<EnemyGenericData> EnemiesData;

        [Header("Enemy Base")]
        public bool BlockSpawnNormal;
        public int MaxNormalEnemies;
        public int MinNormalEnemies;

        [Header("Elemental Enemy")]
        public bool BlockSpawnElemental;
        public int MaxElementalsEnemies;
        public int MinElementalsEnemies;

        [Header("Enemy Ranged")]
        public bool BlockSpawnRanged;
        public int MaxRangedEnemies;
        public int MinRangedEnemies;

    }
}