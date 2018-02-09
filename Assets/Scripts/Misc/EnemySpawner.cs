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

            //TODO: da rivedere per migliorare
            for (int i = 0; i < SpawnPoints.Count / 2; i++)
            {
                int spawnIndex = Random.Range(0, SpawnPoints.Count);

                if(Vector3.Distance(SpawnPoints[spawnIndex].position, target.transform.position) >= MinDistanceSpawn && !spawnAvailable.Contains(SpawnPoints[spawnIndex]))
                    spawnAvailable.Add(SpawnPoints[spawnIndex]);
                else
                {
                    spawnIndex++;
                    if (SpawnPoints[spawnIndex] == null)
                        return;
                    if (Vector3.Distance(SpawnPoints[spawnIndex].position, target.transform.position) >= MinDistanceSpawn && !spawnAvailable.Contains(SpawnPoints[spawnIndex]))
                        spawnAvailable.Add(SpawnPoints[spawnIndex]);
                }

                //while (Vector3.Distance(SpawnPoints[spawnIndex].position, target.transform.position) <= 50 || !spawnAvailable.Contains(SpawnPoints[spawnIndex]))
                //{
                //    spawnIndex = Random.Range(0, SpawnPoints.Count);
                //}

            }

            foreach (Transform spawn in spawnAvailable)
            {
                int hordeNumber = Random.Range(MinHordeNumber, MaxHordeNumber + 1);
                print(hordeNumber);
                for (int i = 0; i < hordeNumber; i++)
                {
                    Spawn(_enemyPrefab, spawn);
                }
            }
        }

        void Spawn(Enemy _enemyPrefab, Transform _spawnPoint)
        {
            if (SpawnPoints.Count == 0)
                SpawnPoints.Add(transform);
            Enemy newEnemy = Instantiate(_enemyPrefab, _spawnPoint.transform.position, Quaternion.identity);
            enemiesSpawned.Add(newEnemy);
            newEnemy.Init(target, this, "Enemy" + idCounter, ChoiseRandomElement());
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
            StartCoroutine(FirstSpawn());
        }

        public void DeleteSpecificEnemy(string _idEnemy)
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

        public void DeleteAllEnemy()
        {
            for (int i = 0; i < enemiesSpawned.Count; i++)
            {
                Destroy(enemiesSpawned[i].gameObject);
            }
        }
        #endregion
    }
}