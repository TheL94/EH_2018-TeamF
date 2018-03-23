using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TeamF
{
    public class EnemyManager : MonoBehaviour
    {   
        public IDamageable EnemyTarget { get; private set; }

        public EnemyManagerData Data;
        public EnemyManagerData DataInstance { get; set; }

        void Update()
        {
            if (!CanSpawn)
                return;

            spawnTime += Time.deltaTime;
            if (spawnTime >= DataInstance.DelayHordes && EnemyTarget.Life > 0)
            {
                SpawnHorde();
                spawnTime = 0;
            }
        }

        #region API
        public virtual void Init(IDamageable _enemyTarget, bool _isTestScene = false)
        {
            Init(_enemyTarget, Instantiate(Data), _isTestScene);
        }

        public virtual void Init(IDamageable _enemyTarget, EnemyManagerData _dataInstance, bool _isTestScene = false)
        {
            DataInstance = _dataInstance;
            EnemyTarget = _enemyTarget;

            Enemy.EnemyDeath += OnEnemyDeath;

            if (!_isTestScene)
                StartCoroutine(FirstSpawn());
        }

        public void InitDataForTestScene()
        {
            DataInstance = Instantiate(Data);
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
        public virtual void OnEnemyDeath(Enemy _enemyKilled)
        {
            Events_LevelController.UpdateKillPoints(_enemyKilled.Data.EnemyValue);
            DeleteSpecificEnemy(_enemyKilled.ID);
        }

        public virtual IDamageable GetTarget(Enemy _enemy)
        {
            if (_enemy.IsCharmed)
                return GetClosestTarget(_enemy);

            if (GameManager.I.Player.Character != null)
                return GameManager.I.Player.Character;

            return null;
        }

        /// <summary>
        /// Torna il nemico più vicino al nemico che lo richiede.
        /// </summary>
        /// <param name="_enemy">Il nemico che richiede il calcolo</param>
        /// <returns></returns>
        IDamageable GetClosestTarget(Enemy _enemy)
        {
            float referanceDistance = 1000;
            IDamageable enemyCloser = null;

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
            float playerDistance = Vector3.Distance(GameManager.I.Player.Character.transform.position, _enemy.transform.position);
            if(playerDistance < referanceDistance)
                enemyCloser = GameManager.I.Player.Character;


            return enemyCloser;
        }

        void DeleteSpecificEnemy(string _idEnemy)
        {
            for (int i = 0; i < enemiesSpawned.Count; i++)
            {
                if (enemiesSpawned[i].ID == _idEnemy)
                {
                    Destroy(enemiesSpawned[i]);
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
        public List<Transform> SpawnPoints = new List<Transform>();
        protected List<Enemy> enemiesSpawned = new List<Enemy>();
        protected int idCounter;
        float spawnTime;

        /// <summary>
        /// Chiama lo spawn della prima orda
        /// </summary>
        /// <returns></returns>
        IEnumerator FirstSpawn()
        {
            yield return new WaitForSeconds(DataInstance.StartDelayTime);
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

                if (!DataInstance.BlockSpawnElemental)
                {
                    int elementalsEnemies = Random.Range(DataInstance.MinElementalsEnemies, DataInstance.MaxElementalsEnemies + 1);

                    for (int j = 0; j < elementalsEnemies; j++)
                    {
                        EnemyData data = FindEnemyDataByType((EnemyType)Random.Range(2, 6));
                        Enemy newEnemy = SpawnEnemy(data.ContainerPrefab, SpawnPoints[i]);
                        if (newEnemy != null)
                            InitEnemy(newEnemy, data);
                    } 
                }

                if (!DataInstance.BlockSpawnRanged)
                {
                    int rangedEnemies = Random.Range(DataInstance.MinRangedEnemies, DataInstance.MaxRangedEnemies + 1);

                    for (int j = 0; j < rangedEnemies; j++)
                    {
                        EnemyData data = FindEnemyDataByType(EnemyType.Ranged);
                        Enemy newEnemy = SpawnEnemy(data.ContainerPrefab, SpawnPoints[i]);
                        if(newEnemy != null)
                            InitEnemy(newEnemy, data);
                    } 
                }

                if (!DataInstance.BlockSpawnNormal)
                {
                    int hordeNumber = Random.Range(DataInstance.MinNormalEnemies, DataInstance.MaxNormalEnemies + 1);

                    for (int j = 0; j < hordeNumber; j++)
                    {
                        EnemyData data = FindEnemyDataByType(EnemyType.Melee);
                        Enemy newEnemy = SpawnEnemy(data.ContainerPrefab, SpawnPoints[i]);
                        if (newEnemy != null)
                            InitEnemy(newEnemy, data);
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
        protected Enemy SpawnEnemy(GameObject _enemyPrefab, Transform _spawnPoint)
        {
            if (enemiesSpawned.Count >= DataInstance.MaxEnemiesInScene)
                return null;

            Enemy newEnemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity, transform).GetComponent<Enemy>();
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
            _enemy.Init(_enemyData, DataInstance.EnemyInitialState, "Enemy" + idCounter);
        }

        /// <summary>
        /// Ritorna un EnemyData che corrisponde ai parametri passati, se esiste
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_element"></param>
        /// <returns></returns>
        protected EnemyData FindEnemyDataByType(EnemyType _type)
        {
            return Instantiate(DataInstance.EnemiesData.Where(d => d.EnemyType == _type).First());
        }
        #endregion

        private void OnDisable()
        {
            Enemy.EnemyDeath -= OnEnemyDeath;
        }
    }
}