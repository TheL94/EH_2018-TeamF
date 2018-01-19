using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Player target;
    public Enemy EnemyPrefab;
    public float DelayTime;
    public List<GameObject> SpawnPoints = new List<GameObject>();

    float time;

    void Update ()
    {
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
        newEnemy.Init(target);
    }
}
