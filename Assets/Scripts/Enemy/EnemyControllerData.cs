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

        public int MaxHordeNumber;
        public int MinHordeNumber;

        public int MaxElementalsEnemies;
        public int MinElementalsEnemies;

        public int MaxRangedEnemies;
        public int MinRangedEnemies;

        public List<EnemyData> EnemiesData;
    }
}