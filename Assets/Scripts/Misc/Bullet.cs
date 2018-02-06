using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Bullet : MonoBehaviour
    {
        public float BulletLife;
        int Damage;
        float Speed;

        private void Start()
        {
            Destroy(gameObject, BulletLife);
        }

        void FixedUpdate()
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
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(Damage);
            }
            Destroy(gameObject);
        }
    }
}