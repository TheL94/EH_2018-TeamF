using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Bullet : MonoBehaviour
    {
        ElementalAmmo ammo;
        public float BulletLife;
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

        public void Init(ElementalAmmo _currentAmmo, float _speed)
        {
            ammo = _currentAmmo;
            Speed = _speed;
        }

        #endregion

        void Move()
        {
            transform.Translate(-transform.forward * Speed);
        }

        void DoDamage(IDamageable _damageable)
        {
            if (_damageable != null)
            {
                _damageable.TakeDamage(ammo.Damage, ammo.AmmoType);              
            }
        }

        void ApplyElementalEffect(Enemy _enemy)
        {
            if (_enemy != null)
            {
                ElementalEffect _effect = _enemy.GetComponent<ElementalEffect>();
                switch (ammo.AmmoType)
                {
                    case ElementalType.Fire:
                        _effect.Init(new ElementalEffectFire(), _enemy, ammo.Data);
                        break;
                    case ElementalType.Water:
                        _effect.Init(new ElementalEffectWater(), _enemy, ammo.Data);
                        break;
                    case ElementalType.Poison:
                        _effect.Init(new ElementalEffectPoison(), _enemy, ammo.Data);
                        break;
                    case ElementalType.Thunder:
                        _effect.Init(new ElementalEffectThunder(), _enemy, ammo.Data);
                        break;
                    case ElementalType.None:
                        break;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            DoDamage(other.GetComponent<IDamageable>());
            ApplyElementalEffect(other.GetComponent<Enemy>());
            //Destroy(gameObject);
        }
    }
}