using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletLife;
    int Damage;
    float Speed;

    private void Start()
    {
        Destroy(gameObject, BulletLife);
    }

    void FixedUpdate ()
    {
        Move();
    }

    #region API

    public void Init(int _damage, float _speed)
    {
        Damage = _damage;
        Speed = _speed;
    }

    #endregion

    void Move()
    {
        transform.Translate(-transform.forward * Speed); 
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemyHit = other.GetComponent<Enemy>();
        if(enemyHit != null)
        {
            enemyHit.TakeDamage(Damage);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
