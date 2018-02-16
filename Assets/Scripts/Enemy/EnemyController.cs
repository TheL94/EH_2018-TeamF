using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyController : MonoBehaviour
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

        LevelManager levelMng;

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

        /// <summary>
        /// Determina quale spawn point è il più vicino al player e lo esclude, spawna i nemici sugli spawn points rimasti
        /// </summary>
        /// <param name="_enemyPrefab"></param>
        void SpawnHorde(Enemy _enemyPrefab)
        {
            int spawnIndexToExclude = 0;

            float _distance = 100000;

            for (int i = 0; i < SpawnPoints.Count; i++)
            {
                float _spawnDistance = Vector3.Distance(SpawnPoints[i].position, target.transform.position);
                if (_spawnDistance < _distance)
                {
                    spawnIndexToExclude = i;
                    _distance = _spawnDistance;
                }
            }

            for (int i = 0; i < SpawnPoints.Count; i++)
            {
                if (i == spawnIndexToExclude)
                    continue;

                int hordeNumber = Random.Range(MinHordeNumber, MaxHordeNumber + 1);
                int elementalsEnemies = Random.Range(MinElementalsEnemies, MaxElementalsEnemies + 1);

                for (int j = 0; j < elementalsEnemies; j++)
                {
                    Spawn(_enemyPrefab, SpawnPoints[i], true);
                }

                for (int j = 0; j < hordeNumber - elementalsEnemies; j++)
                {
                    Spawn(_enemyPrefab, SpawnPoints[i]);
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
                newEnemy.Init(target, this, "Enemy" + idCounter, new EnemyBehaviourBase());

            idCounter++;
        }

        IEnemyBehaviour ChoiseRandomElement()
        {
            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0:
                    return new FireBehaviour();
                
                default:
                    return new EnemyBehaviourBase();
            }
        }

        IEnumerator FirstSpawn()
        {
            yield return new WaitForSeconds(StartDelayTime);
            SpawnHorde(EnemyPrefab);
            CanSpawn = true;
        }

        #region API
        public void Init(LevelManager _levelMng)
        {
            levelMng = _levelMng;
            StartCoroutine(FirstSpawn());
        }

        /// <summary>
        /// Cancella il nemico dalla lista di nemici spawnati, aggiunge il valore del nemico al contatore dei nemici uccisi, 
        /// se la partita è vinta avvisa il gamemanager
        /// </summary>
        /// <param name="_enemyKilled"></param>
        public void KillEnemy(Enemy _enemyKilled)
        {
            levelMng.UpdateRoundPoints(_enemyKilled.EnemyValue);
            DeleteSpecificEnemy(_enemyKilled.SpecificID);
        }

        /// <summary>
        /// Blocca lo spawn di altri nemici e cancella quelli presenti in scena
        /// </summary>
        public void EndGameplayActions()
        {
            CanSpawn = false;
            DeleteAllEnemies();
        }
        #endregion

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
            enemiesSpawned.Clear();
        }
    }
}