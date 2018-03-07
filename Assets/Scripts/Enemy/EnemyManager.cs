using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TeamF
{
    public class EnemyManager : MonoBehaviour
    {
        public Enemy EnemyPrefab;     
        public IDamageable EnemyTarget { get; private set; }

        void Update()
        {
            if (!CanSpawn)
                return;

            spawnTime += Time.deltaTime;
            if (spawnTime >= Data.DelayHordes && EnemyTarget.Life > 0)
            {
                SpawnHorde();
                spawnTime = 0;
            }
        }

        #region API
        public virtual void Init(IDamageable _enemyTarget)
        {
            EnemyTarget = _enemyTarget;
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

        #region Enemy
        /// <summary>
        /// Cancella il nemico dalla lista di nemici spawnati, aggiunge il valore del nemico al contatore dei nemici uccisi, 
        /// se la partita è vinta avvisa il gamemanager
        /// </summary>
        /// <param name="_enemyKilled"></param>
        public void KillEnemy(Enemy _enemyKilled)
        {
            Events_LevelController.UpdateKillPoints(_enemyKilled.Data.EnemyValue);
            DeleteSpecificEnemy(_enemyKilled.ID);
        }

        /// <summary>
        /// Torna il nemico più vicino al nemico che lo richiede.
        /// </summary>
        /// <param name="_enemy">Il nemico che richiede il calcolo</param>
        /// <returns></returns>
        public Enemy GetClosestTarget(Enemy _enemy)
        {
            float referanceDistance = 1000;
            Enemy enemyCloser = null;

            foreach (Enemy enemy in enemiesSpawned)
            {
                if (_enemy.ID == enemy.ID)
                    continue;

                float distance = Vector3.Distance(_enemy.transform.position, enemy.transform.position);
                if (distance < referanceDistance)
                {
                    enemyCloser = enemy;
                    referanceDistance = distance;
                }
            }
            return enemyCloser;
        }

        void DeleteSpecificEnemy(string _idEnemy)
        {
            for (int i = 0; i < enemiesSpawned.Count; i++)
            {
                if (enemiesSpawned[i].ID == _idEnemy)
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
        #endregion

        #region Spawner
        public bool CanSpawn { get; set; }
        public EnemyManagerData Data;
        public List<Transform> SpawnPoints = new List<Transform>();
        List<Enemy> enemiesSpawned = new List<Enemy>();
        protected int idCounter;
        float spawnTime;

        /// <summary>
        /// Chiama lo spawn della prima orda
        /// </summary>
        /// <returns></returns>
        IEnumerator FirstSpawn()
        {
            yield return new WaitForSeconds(Data.StartDelayTime);
            SpawnHorde();
            CanSpawn = true;
        }

        /// <summary>
        /// Determina quale spawn point è il più vicino al player e lo esclude, spawna i nemici sugli spawn points rimasti
        /// </summary>
        /// <param name="_enemyPrefab"></param>
        void SpawnHorde()
        {
            int spawnIndexToExclude = ChooseSpawnPointToExclude();

            for (int i = 0; i < SpawnPoints.Count; i++)
            {
                if (i == spawnIndexToExclude)
                    continue;

                if (!Data.BlockSpawnElemental)
                {
                    int elementalsEnemies = Random.Range(Data.MinElementalsEnemies, Data.MaxElementalsEnemies + 1);

                    for (int j = 0; j < elementalsEnemies; j++)
                    {
                        Enemy newEnemy = SpawnEnemy(EnemyPrefab, SpawnPoints[i]);
                        if (newEnemy != null)
                            InitEnemy(newEnemy, FindEnemyDataByTypeAndElement(EnemyType.Melee, (ElementalType)Random.Range(1, 5)));
                    } 
                }

                if (!Data.BlockSpawnRanged)
                {
                    int rangedEnemies = Random.Range(Data.MinRangedEnemies, Data.MaxRangedEnemies + 1);

                    for (int j = 0; j < rangedEnemies; j++)
                    {
                        Enemy newEnemy = SpawnEnemy(EnemyPrefab, SpawnPoints[i]);
                        if(newEnemy != null)
                            InitEnemy(newEnemy, FindEnemyDataByTypeAndElement(EnemyType.Ranged, ElementalType.None));
                    } 
                }

                if (!Data.BlockSpawnNormal)
                {
                    int hordeNumber = Random.Range(Data.MinNormalEnemies, Data.MaxNormalEnemies + 1);

                    for (int j = 0; j < hordeNumber; j++)
                    {
                        Enemy newEnemy = SpawnEnemy(EnemyPrefab, SpawnPoints[i]);
                        if (newEnemy != null)
                            InitEnemy(newEnemy, FindEnemyDataByTypeAndElement(EnemyType.Melee, ElementalType.None));
                    } 
                }
            }
        }

        /// <summary>
        /// Sceglie lo spawn point da disattivare, cercando quello più vicino al player
        /// </summary>
        /// <returns></returns>
        int ChooseSpawnPointToExclude()
        {
            int spawnIndexToExclude = 0;
            float _distance = 100000;                   // Distanza improbabile, la prima distanza di riferimento è enorme

            for (int i = 0; i < SpawnPoints.Count; i++)
            {
                float _spawnDistance = Vector3.Distance(SpawnPoints[i].position, EnemyTarget.Position);
                if (_spawnDistance < _distance)
                {
                    spawnIndexToExclude = i;
                    _distance = _spawnDistance;
                }
            }

            return spawnIndexToExclude;
        }

        /// <summary>
        /// Instanza un nuovo nemico
        /// </summary>
        /// <param name="_enemyPrefab">Il prefab del nemico da utilizzare</param>
        /// <param name="_spawnPoint">Lo spawn point dove far spawnare il nemico</param>
        /// <param name="SpawnElementalEnemy">True se il nemico da spawnare è elementale</param>
        protected Enemy SpawnEnemy(Enemy _enemyPrefab, Transform _spawnPoint)
        {
            if (enemiesSpawned.Count >= Data.MaxEnemiesInScene)
                return null;

            Enemy newEnemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity, transform);
            enemiesSpawned.Add(newEnemy);

            idCounter++;
            return newEnemy;
        }

        /// <summary>
        /// Inizializza nemico
        /// </summary>
        /// <param name="_enemy"></param>
        /// <param name="_enemyData"></param>
        void InitEnemy(Enemy _enemy, EnemyData _enemyData)
        {
            _enemy.Init(EnemyTarget, this, _enemyData, Data.EnemyInitialState, "Enemy" + idCounter);
        }

        /// <summary>
        /// Ritorna un EnemyData che corrisponde ai parametri passati, se esiste
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_element"></param>
        /// <returns></returns>
        protected EnemyData FindEnemyDataByTypeAndElement(EnemyType _type, ElementalType _element)
        {
            List<EnemyData> sortedByType = Data.EnemiesData.Where(d => d.EnemyType == _type).ToList();
            List<EnemyData> sortedByElement = new List<EnemyData>();

            if (sortedByType.Count == 1)
            {
                return Instantiate(sortedByType[0]);
            }
            else
            {
                sortedByElement = sortedByType.Where(d => d.ElementalType == _element).ToList();
                if (sortedByElement.Count == 1)
                    return Instantiate(sortedByElement[0]);
            }

            return null;
        }
        #endregion
    }
}