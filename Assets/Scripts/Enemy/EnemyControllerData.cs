using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    [CreateAssetMenu(fileName = "SpawnerData", menuName = "EnemySpawner/SpawnerData", order = 1)]
    public class EnemyControllerData : ScriptableObject
    {
        public float StartDelayTime;
        public float DelayHordes;
        public int MaxEnemiesInScene;

        public bool BlockSpawnNormal;
        public int MaxNormalEnemies;
        public int MinNormalEnemies;

        public bool BlockSpawnElemental;
        public int MaxElementalsEnemies;
        public int MinElementalsEnemies;

        public bool BlockSpawnRanged;
        public int MaxRangedEnemies;
        public int MinRangedEnemies;

        public List<EnemyData> EnemiesData;
    }
}