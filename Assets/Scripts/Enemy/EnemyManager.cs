using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TeamF
{
    public class EnemyManager : MonoBehaviour
    {
        public EnemyManagerData Data;
        public EnemyManagerData DataInstance { get; set; }

        private void OnEnable()
        {
            Enemy.EnemyDeath += OnEnemyDeath;
            Enemy.UpdateKill += UpdateKillPoints;
        }

        void OnDisable()
        {
            Enemy.EnemyDeath -= OnEnemyDeath;
            Enemy.UpdateKill -= UpdateKillPoints;
        }

        void Update()
        {
            if (!CanSpawn)
                return;

            spawnTime += Time.deltaTime;

            if (spawnTime >= DataInstance.DelayHordes && (GameManager.I.CurrentState == FlowState.Gameplay || GameManager.I.CurrentState == FlowState.TestGameplay))
            {
                StartCoroutine(ActiveSpawnParticles());
                spawnTime = 0;
            }
        }

        #region API
        public void Init(bool _isTestScene = false)
        {
            Init(Instantiate(Data), _isTestScene);
        }

        public void Init(EnemyManagerData _dataInstance, bool _isTestScene = false)
        {
            DataInstance = _dataInstance;

            if (!_isTestScene)
            {
                GetSpawnInScene();
                StartCoroutine(FirstSpawn());
            }
        }

        /// <summary>
        /// Blocca lo spawn di altri nemici e cancella quelli presenti in scena
        /// </summary>
        public void EndGameplayActions()
        {
            ToggleAllAIs(false);
            CanSpawn = false;
            DeleteAllEnemies();
            spawnPoints.Clear();
        }
        #endregion

        #region Enemy
        /// <summary>
        /// Cancella il nemico dalla lista di nemici spawnati, aggiunge il valore del nemico al contatore dei nemici uccisi, 
        /// se la partita è vinta avvisa il gamemanager.
        /// </summary>
        /// <param name="_enemyKilled"></param>
        public virtual void OnEnemyDeath(Enemy _enemyKilled)
        {
            DeleteSpecificEnemy(_enemyKilled.ID);
        }

        /// <summary>
        /// Chiama l'evento per aggiornare i punti vittoria
        /// </summary>
        /// <param name="_enemyKilled"></param>
        public void UpdateKillPoints(Enemy _enemyKilled)
        {
            Events_LevelController.UpdateKillPoints(_enemyKilled.Data.EnemyValue);
        }

        /// <summary>
        /// Funizonche setta lo stato acceso o spento di tutte le AI in gioco.
        /// </summary>
        /// <param name="_value"></param>
        public void ToggleAllAIs(bool _value)
        {
            foreach (Enemy enemy in enemiesSpawned)
            {
                enemy.AI_Enemy.IsActive = _value;
                enemy.Agent.enabled = _value;
            }
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
            IDamageable closestTarget = null;

            foreach (Enemy enemy in enemiesSpawned)
            {
                if (_enemy.ID == enemy.ID)
                    continue;

                float distance = Vector3.Distance(_enemy.transform.position, enemy.transform.position);
                if (distance < referanceDistance)
                {
                    closestTarget = enemy;
                    referanceDistance = distance;
                }
            }
            float playerDistance = Vector3.Distance(GameManager.I.Player.Character.transform.position, _enemy.transform.position);
            if (playerDistance < referanceDistance)
                closestTarget = GameManager.I.Player.Character;


            return closestTarget;
        }

        void DeleteSpecificEnemy(string _idEnemy)
        {
            for (int i = 0; i < enemiesSpawned.Count; i++)
            {
                if (enemiesSpawned[i].ID == _idEnemy)
                {
                    Enemy enemyToDestroy = enemiesSpawned[i];
                    enemyToDestroy.gameObject.SetActive(false);
                    GameManager.I.PoolMng.UpdatePool(enemyToDestroy.Data.GraphicID);

                    enemiesSpawned.Remove(enemiesSpawned[i]);
                    Destroy(enemyToDestroy.gameObject, 0.1f);
                    return;
                }
            }
        }

        void DeleteAllEnemies()
        {
            //GameManager.I.PoolMng.ForcePoolReset();

            for (int i = 0; i < enemiesSpawned.Count; i++)
            {
                enemiesSpawned[i].gameObject.SetActive(false);
                GameManager.I.PoolMng.UpdatePool(enemiesSpawned[i].Data.GraphicID);
                Destroy(enemiesSpawned[i].gameObject, 0.1f);
            }
            enemiesSpawned.Clear();
        }
        #endregion

        #region Spawner
        public bool CanSpawn { get; set; }
        List<Transform> spawnPoints = new List<Transform>();
        List<Enemy> enemiesSpawned = new List<Enemy>();
        int idCounter;
        float spawnTime;

        /// <summary>
        /// Chiama lo spawn della prima ordaMO
        /// </summary>
        /// <returns></returns>
        IEnumerator FirstSpawn()
        {
            SetSpawnParticles(true);
            yield return new WaitForSeconds(DataInstance.StartDelayTime);
            SpawnHorde();
            CanSpawn = true;
            SetSpawnParticles(false);
        }

        /// <summary>
        /// Determina quale spawn point è il più vicino al player e lo esclude, spawna i nemici sugli spawn points rimasti
        /// </summary>
        /// <param name="_enemyPrefab"></param>
        void SpawnHorde()
        {
            int spawnIndexToExclude = ChooseSpawnPointToExclude();

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                if (i == spawnIndexToExclude)
                    continue;

                if (!DataInstance.BlockSpawnElemental)
                {
                    int elementalsEnemies = Random.Range(DataInstance.MinElementalsEnemies, DataInstance.MaxElementalsEnemies + 1);

                    for (int j = 0; j < elementalsEnemies; j++)
                    {
                        EnemyGenericData data = FindEnemyDataByType((EnemyType)Random.Range(2, 6));
                        if (data != null)
                        {
                            Enemy newEnemy = SpawnEnemy(data.ContainerPrefab, spawnPoints[i]);
                            if (newEnemy != null)
                                InitEnemy(newEnemy, data);
                        }
                    }
                }

                if (!DataInstance.BlockSpawnRanged)
                {
                    int rangedEnemies = Random.Range(DataInstance.MinRangedEnemies, DataInstance.MaxRangedEnemies + 1);

                    for (int j = 0; j < rangedEnemies; j++)
                    {
                        EnemyGenericData data = FindEnemyDataByType(EnemyType.Ranged);
                        if (data != null)
                        {
                            Enemy newEnemy = SpawnEnemy(data.ContainerPrefab, spawnPoints[i]);
                            if (newEnemy != null)
                                InitEnemy(newEnemy, data);
                        }
                    }
                }

                if (!DataInstance.BlockSpawnNormal)
                {
                    int hordeNumber = Random.Range(DataInstance.MinNormalEnemies, DataInstance.MaxNormalEnemies + 1);

                    for (int j = 0; j < hordeNumber; j++)
                    {
                        EnemyGenericData data = FindEnemyDataByType(EnemyType.Melee);
                        if (data != null)
                        {
                            Enemy newEnemy = SpawnEnemy(data.ContainerPrefab, spawnPoints[i]);
                            if (newEnemy != null)
                                InitEnemy(newEnemy, data);
                        }
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

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                float _spawnDistance = Vector3.Distance(spawnPoints[i].position, GameManager.I.Player.Character.Position);
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
        Enemy SpawnEnemy(GameObject _enemyPrefab, Transform _spawnPoint)
        {
            if (enemiesSpawned.Count >= DataInstance.MaxEnemiesInScene)
                return null;

            Enemy newEnemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity, _spawnPoint).GetComponent<Enemy>();
            enemiesSpawned.Add(newEnemy);

            idCounter++;
            return newEnemy;
        }

        /// <summary>
        /// Inizializza nemico
        /// </summary>
        /// <param name="_enemy"></param>
        /// <param name="_enemyData"></param>
        void InitEnemy(Enemy _enemy, EnemyGenericData _enemyData)
        {
            _enemy.Init(_enemyData, "Enemy" + idCounter);
        }

        /// <summary>
        /// Ritorna un EnemyData che corrisponde ai parametri passati, se esiste
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_element"></param>
        /// <returns></returns>
        EnemyGenericData FindEnemyDataByType(EnemyType _type)
        {
            EnemyGenericData data = DataInstance.EnemiesData.Where(d => d.EnemyType == _type).FirstOrDefault();
            if (data != null)
                return Instantiate(data);
            else
                return null;
        }

        void GetSpawnInScene()
        {
            if (spawnPoints.Count > 0)
                spawnPoints.Clear();
            foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("EnemySpawn"))
            {
                spawnPoints.Add(spawn.transform);
            }
        }
        #endregion

        #region Test Scene
        public bool IgnoreTarget;

        public void SpawnEnemyForTestScene()
        {
            GetSpawnInScene();

            for (int i = 2; i < Data.EnemiesData.Count; i++)
            {
                EnemyGenericData data = FindEnemyDataByType((EnemyType)i);
                if (data != null)
                {
                    Transform spawn;
                    if (i - 2 < spawnPoints.Count)
                        spawn = spawnPoints[i - 2];
                    else
                        spawn = spawnPoints[0];

                    Enemy _newEnemy = SpawnEnemy(data.ContainerPrefab, spawn);
                    _newEnemy.Init(data, "Enemy" + idCounter);
                }
            }
        }
        #endregion

        void SetSpawnParticles(bool _active)
        {
            foreach (Transform spawn in spawnPoints)
            {
                foreach (ParticleSystem particle in spawn.GetComponentsInChildren<ParticleSystem>())
                {
                    if (particle != null)
                    {
                        if (_active)
                            particle.Play();
                        else
                            particle.Stop();
                    }
                }
            }
        }

        IEnumerator ActiveSpawnParticles()
        {
            SetSpawnParticles(true);
            yield return new WaitForSeconds(2f);
            SpawnHorde();
            SetSpawnParticles(false);
        }
    }
}