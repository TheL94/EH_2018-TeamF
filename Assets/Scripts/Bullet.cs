using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rigid;
    public float Speed;
    public float BulletLife;
    public int Damage;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Destroy(gameObject, BulletLife);
    }

    void FixedUpdate ()
    {
        rigid.AddRelativeForce(Vector3.up * Speed, ForceMode.Impulse);
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
