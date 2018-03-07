using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemySpawner_TS : EnemyManager
    {
        public List<IDamageable> ManichiniDiDestinazione = new List<IDamageable>();

        [HideInInspector]
        public bool FollowPlayer;

        public override void Init(IDamageable _enemyTarget, bool _isTestScene = false)
        {
            base.Init(_enemyTarget, _isTestScene);

            foreach (IDamageable dummy in GetComponentsInChildren<IDamageable>())
            {
                ManichiniDiDestinazione.Add(dummy);
            }

            SpawnEnemyForeachSpawn();
        }

        void SpawnEnemyForeachSpawn()
        {
            for (int i = 0; i < SpawnPoints.Count; i++)
            {
                SpawnEnemy(SpawnPoints[i], ManichiniDiDestinazione[i], (ElementalType)i + 1);
            }
        }

        /// <summary>
        /// Crea un nuovo nemico e gli chiama l'init
        /// </summary>
        /// <param name="_spawner">Lo spawn point dove deve essere spawnatp</param>
        /// <param name="_dummy">Il manichino da attaccare, se non deve seguire il player ma andare in un punto della mappa</param>
        /// <param name="_type">Il Tipo del nemico</param>
        void SpawnEnemy(Transform _spawner, IDamageable _dummy, ElementalType _type)
        {
            Enemy _newEnemy = SpawnEnemy(EnemyPrefab, _spawner);
            if (!FollowPlayer)
                _newEnemy.Init(_dummy, FindEnemyDataByTypeAndElement(EnemyType.Melee, _type), Data.EnemyInitialState, "Enemy" + idCounter);
            else
                _newEnemy.Init(EnemyTarget, FindEnemyDataByTypeAndElement(EnemyType.Melee, _type), Data.EnemyInitialState, "Enemy" + idCounter);
        }

        public override void OnEnemyDeath(Enemy _enemyKilled)
        {
            
        }
    }
}