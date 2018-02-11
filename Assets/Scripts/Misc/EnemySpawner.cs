using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemySpawner : MonoBehaviour
    {
        public AvatarController target;
        public Enemy EnemyPrefab;
        public float StartDelayTime;
        public float DelayHordes;
        public float MinDistanceSpawn;
        public bool CanSpawn { get; set; }

        public int MaxHordeNumber;
        public int MinHordeNumber;

        public int MaxElementalsEnemies;
        public int MinElementalsEnemies;

        public float PointsToWin;
        float roundPoints;

        public List<Transform> SpawnPoints = new List<Transform>();
        List<Enemy> enemiesSpawned = new List<Enemy>();
        int idCounter;

        float time;

        void Update()
        {
            if (!CanSpawn)
                return;

            time += Time.deltaTime;
            if (time >= DelayHordes && target.Life > 0)
            {
                SpawnHorde(EnemyPrefab);
                time = 0;
            }
        }

        void SpawnHorde(Enemy _enemyPrefab)
        {
            List<Transform> spawnAvailable = new List<Transform>();

            int spawnIndexToExclude = Random.Range(0, SpawnPoints.Count);

            for (int i = 0; i < SpawnPoints.Count; i++)
            {
                if (i == spawnIndexToExclude)
                    continue;

                if (Vector3.Distance(SpawnPoints[i].position, target.transform.position) >= MinDistanceSpawn)
                    spawnAvailable.Add(SpawnPoints[i]);
                else
                    spawnAvailable.Add(SpawnPoints[spawnIndexToExclude]);
            }

            foreach (Transform spawn in spawnAvailable)
            {
                int hordeNumber = Random.Range(MinHordeNumber, MaxHordeNumber + 1);
                int elementalsEnemies = Random.Range(MinElementalsEnemies, MaxElementalsEnemies + 1);

                for (int i = 0; i < elementalsEnemies; i++)
                {
                    Spawn(_enemyPrefab, spawn, true);
                }

                for (int i = 0; i < hordeNumber - elementalsEnemies; i++)
                {
                    Spawn(_enemyPrefab, spawn);
                }
            }
        }

        /// <summary>
        /// Instanza un nuovo nemico e ne chiama l'Init
        /// </summary>
        /// <param name="_enemyPrefab">Il prefab del nemico da utilizzare</param>
        /// <param name="_spawnPoint">Lo spawn point dove far spawnare il nemico</param>
        /// <param name="SpawnElementalEnemy">True se il nemico da spawnare è elementale</param>
        void Spawn(Enemy _enemyPrefab, Transform _spawnPoint, bool SpawnElementalEnemy = false)
        {
            if (SpawnPoints.Count == 0)
                SpawnPoints.Add(transform);
            Enemy newEnemy = Instantiate(_enemyPrefab, _spawnPoint.transform.position, Quaternion.identity, transform);
            enemiesSpawned.Add(newEnemy);

            if (SpawnElementalEnemy)
                newEnemy.Init(target, this, "Enemy" + idCounter, ChoiseRandomElement()); 
            else
                newEnemy.Init(target, this, "Enemy" + idCounter);

            idCounter++;
        }

        ElementalType ChoiseRandomElement()
        {
            return (ElementalType)Random.Range(0, 4);
        }

        IEnumerator FirstSpawn()
        {
            yield return new WaitForSeconds(StartDelayTime);
            SpawnHorde(EnemyPrefab);
            CanSpawn = true;
        }

        #region API

        public void Init()
        {
            roundPoints = 0;
            StartCoroutine(FirstSpawn());
        }

        public void KillEnemy(Enemy _enemyKilled)
        {
            roundPoints += _enemyKilled.EnemyValue;
            DeleteSpecificEnemy(_enemyKilled.SpecificID);
            if (CheckVictory())
            {
                GameManager.I.VictoryActions();
            }
        }

        public void EndGameActions()
        {
            CanSpawn = false;
            DeleteAllEnemies();
        }
        #endregion

        bool CheckVictory()
        {
            if (roundPoints >= PointsToWin)
                return true;
            else
                return false;
        }

        void DeleteSpecificEnemy(string _idEnemy)
        {
            for (int i = 0; i < enemiesSpawned.Count; i++)
            {
                if (enemiesSpawned[i].SpecificID == _idEnemy)
                {
                    enemiesSpawned.Remove(enemiesSpawned[i]);
                    return;
                }
            }
        }

        void DeleteAllEnemies()
        {
            for (int i = 0; i < enemiesSpawned.Count; i++)
            {
                Destroy(enemiesSpawned[i].gameObject);
            }
        }
    }
}