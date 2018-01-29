using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Player target;
    public Enemy EnemyPrefab;
    public float DelayTime;
    public List<GameObject> SpawnPoints = new List<GameObject>();
    List<Enemy> enemiesSpawned = new List<Enemy>();
    int idCounter;
    float time;

    void Update ()
    {
        if (GameManager.I.CurrentState != FlowState.Gameplay)
            return;

        time += Time.deltaTime;
        if(time >= DelayTime && target.Life > 0)
        {
            Spawn(EnemyPrefab);
            time = 0;
        }
	}
	
	void Spawn(Enemy _enemyPrefab)
    {
        if (SpawnPoints.Count == 0)
            SpawnPoints.Add(gameObject);
        Enemy newEnemy = Instantiate(_enemyPrefab, SpawnPoints[Random.Range(0, SpawnPoints.Count)].transform.position, Quaternion.identity);
        enemiesSpawned.Add(newEnemy);
        newEnemy.Init(target,this, "Enemy" + idCounter);
        idCounter++;
    }

    #region API

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
