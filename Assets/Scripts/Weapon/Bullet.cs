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

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(ammo.Damage, ammo.AmmoType);
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    ElementalEffect _effect = other.gameObject.GetComponent<ElementalEffect>();
                    switch (ammo.AmmoType)
                    {
                        case ElementalType.Fire:
                            _effect.Init(new ElementalEffectFire(), enemy, ammo.Data);
                            break;
                        case ElementalType.Water:
                            _effect.Init(new ElementalEffectWater(), enemy, ammo.Data);
                            break;
                        case ElementalType.Poison:
                            _effect.Init(new ElementalEffectPoison(), enemy, ammo.Data);
                            break;
                        case ElementalType.Thunder:
                            _effect.Init(new ElementalEffectThunder(), enemy, ammo.Data);
                            break;
                        case ElementalType.None:
                            break;
                    } 
                }
            }
            
            

            Destroy(gameObject);
        }
    }
}