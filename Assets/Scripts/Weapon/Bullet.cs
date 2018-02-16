using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Bullet : MonoBehaviour
    {
        ElementalType bulletType;
        public float BulletLife;
        float Damage;
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

        public void Init(float _damage, float _speed, ElementalType _ammoType)
        {
            Damage = _damage;
            Speed = _speed;
            bulletType = _ammoType;
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
                damageable.TakeDamage(Damage, bulletType);
            }
            Destroy(gameObject);
        }
    }
}