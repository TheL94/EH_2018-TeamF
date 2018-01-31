using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public int Life;
    public float MovementSpeed;
    public int Damage;
    public float DamageRange;
    public float DamageRate;

    public string SpecificID { get; set; }

    EnemySpawner spawner;
    Player target;
    float time;

    public void Init(Player _target, EnemySpawner _spawner, string _id)
    {
        target = _target;
        spawner = _spawner;
        SpecificID = _id;
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        GetComponent<NavMeshAgent>().destination = target.transform.position;

        CheckmovementConstrains();
        //Move();
        //Rotate();
        Attack();
    }

    public void TakeDamage(int _damage)
    {
        Life -= _damage;
        if (Life <= 0)
        {
            spawner.DeleteSpecificEnemy(SpecificID);
            Destroy(gameObject);
        }
    }

    void CheckmovementConstrains()
    {
        if (transform.rotation.x != 0 || transform.rotation.z != 0)
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void Move()
    {
        if(DamageRange <= Vector3.Distance(transform.position, target.transform.position))
            transform.position = Vector3.Lerp(transform.position, target.transform.position, (MovementSpeed * Time.deltaTime) / Vector3.Distance(transform.position, target.transform.position));
    }

    void Attack()
    {
        time += Time.deltaTime;
        if (time >= DamageRate)
        {
            if (DamageRange >= Vector3.Distance(transform.position, target.transform.position))
            {
                target.TakeDamage(Damage);
                time = 0;
            }
        }
        
    }

    void Rotate()
    {
        Quaternion.LookRotation(transform.position - target.transform.position);
    }
}
