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
            SpawnEnemy(SpawnPoints[(int)_enemyKilled.Data.ElementalType - 1], EnemyDestinations[(int)_enemyKilled.Data.ElementalType - 1], _enemyKilled.Data.EnemyType);
            enemiesSpawned.Remove(_enemyKilled);
        }

        void SpawnEnemyForeachSpawn()
        {
            for (int i = 0; i < SpawnPoints.Count; i++)
            {
                SpawnEnemy(SpawnPoints[i], EnemyDestinations[i], (EnemyType)i + 2);
            }
        }

        /// <summary>
        /// Crea un nuovo nemico e gli chiama l'init
        /// </summary>
        /// <param name="_spawner">Lo spawn point dove deve essere spawnatp</param>
        /// <param name="_dummy">Il manichino da attaccare, se non deve seguire il player ma andare in un punto della mappa</param>
        /// <param name="_type">Il Tipo del nemico</param>
        void SpawnEnemy(Transform _spawner, IDamageable _dummy, EnemyType _type)
        {
            Enemy _newEnemy = SpawnEnemy(EnemyPrefab, _spawner);
            if (!FollowPlayer)
                _newEnemy.Init(_dummy, FindEnemyDataByType(_type), DataInstance.EnemyInitialState, "Enemy" + idCounter);
            else
                _newEnemy.Init(EnemyTarget, FindEnemyDataByType(_type), DataInstance.EnemyInitialState, "Enemy" + idCounter);
        }

        
    }
}