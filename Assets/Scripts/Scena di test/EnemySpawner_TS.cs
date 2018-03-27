using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemySpawner_TS : EnemyManager
    {
        public List<IDamageable> EnemyDestinations = new List<IDamageable>();

        [HideInInspector]
        public bool FollowPlayer;

        public override void Init(IDamageable _enemyTarget, bool _isTestScene = false)
        {
            base.Init(_enemyTarget, _isTestScene);

            foreach (IDamageable _destination in GetComponentsInChildren<IDamageable>())
            {
                EnemyDestinations.Add(_destination);
            }

            SpawnEnemyForeachSpawn();
        }

        public override void Init(IDamageable _enemyTarget, EnemyManagerData _dataInstance, bool _isTestScene = false)
        {
            base.Init(_enemyTarget, _dataInstance, _isTestScene);

            foreach (IDamageable _destination in GetComponentsInChildren<IDamageable>())
            {
                EnemyDestinations.Add(_destination);
            }

            SpawnEnemyForeachSpawn();
        }

        public override void OnEnemyDeath(Enemy _enemyKilled)
        {
            Destroy(_enemyKilled.gameObject);
            SpawnEnemy(spawnPoints[(int)_enemyKilled.Data.ElementalType - 1], _enemyKilled.Data.EnemyType);
            enemiesSpawned.Remove(_enemyKilled);
        }

        public override IDamageable GetTarget(Enemy _enemy)
        {
            if (FollowPlayer)
                return base.GetTarget(_enemy);
            else
                return null;
        }

        void SpawnEnemyForeachSpawn()
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                SpawnEnemy(spawnPoints[i], (EnemyType)i + 2);
            }
        }

        /// <summary>
        /// Crea un nuovo nemico e gli chiama l'init
        /// </summary>
        /// <param name="_spawner">Lo spawn point dove deve essere spawnatp</param>
        /// <param name="_type">Il Tipo del nemico</param>
        void SpawnEnemy(Transform _spawner, EnemyType _type)
        {
            EnemyData data = FindEnemyDataByType(_type);
            Enemy _newEnemy = SpawnEnemy(data.ContainerPrefab, _spawner);
            _newEnemy.Init(data, "Enemy" + idCounter);
        }     
    }
}