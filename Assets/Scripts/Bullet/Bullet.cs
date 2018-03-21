using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Bullet : MonoBehaviour
    {
        TrailRenderer trail;
        MeshRenderer rend;
        BulletOwner owner;
        ElementalAmmo ammo;
        float Speed;

        IBulletBehaviour behaviour;             // Command for Bullet

        void FixedUpdate()
        {
            Move();
        }

        #region API

        public void Init(ElementalAmmo _currentAmmo, float _speed, BulletOwner _owner, float _bulletLife, IBulletBehaviour _behaviour)
        {
            ammo = _currentAmmo;
            Speed = _speed;
            owner = _owner;
            trail = GetComponentInChildren<TrailRenderer>();
            rend = GetComponentInChildren<MeshRenderer>();
            behaviour = _behaviour;
            SetBulletColors(_currentAmmo.AmmoType);

            Destroy(gameObject,_bulletLife);
        }

        #endregion

        void SetBulletColors(ElementalType _type)
        {
            switch (_type)
            {
                case ElementalType.None:
                    if(rend != null)
                        rend.material.color = Color.grey;
                    if(trail != null)
                    {
                        trail.material.color = Color.grey;
                        trail.material.SetColor("_EmissionColor", Color.grey);
                    }
                    break;
                case ElementalType.Fire:
                    if (rend != null)
                        rend.material.color = Color.red;
                    if (trail != null)
                    {
                        trail.material.color = Color.red;
                        trail.material.SetColor("_EmissionColor", Color.red);
                    }           
                    break;
                case ElementalType.Water:
                    if (rend != null)
                        rend.material.color = Color.blue;
                    if (trail != null)
                    {
                        trail.material.color = Color.blue;
                        trail.material.SetColor("_EmissionColor", Color.blue);
                    }
                    break;
                case ElementalType.Poison:
                    if (rend != null)
                        rend.material.color = Color.green;
                    if (trail != null)
                    {
                        trail.material.color = Color.green;
                        trail.material.SetColor("_EmissionColor", Color.green);
                    }
                    break;
                case ElementalType.Thunder:
                    if (rend != null)
                        rend.material.color = Color.magenta;
                    if (trail != null)
                    {
                        trail.material.color = Color.magenta;
                        trail.material.SetColor("_EmissionColor", Color.magenta);
                    }
                    break;
            }
        }

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
                EffectController _effect = _enemy.GetComponent<EffectController>();
                switch (ammo.AmmoType)
                {
                    case ElementalType.Fire:
                        _effect.InitEffect(new BulletEffectFire(), _enemy, ammo.Data);
                        break;
                    case ElementalType.Water:
                        _effect.InitEffect(new BulletEffectWater(), _enemy, ammo.Data);
                        break;
                    case ElementalType.Poison:
                        _effect.InitEffect(new BulletEffectPoison(), _enemy, ammo.Data);
                        break;
                    case ElementalType.Thunder:
                        _effect.InitEffect(new BulletEffectThunder(), _enemy, ammo.Data);
                        break;
                    case ElementalType.None:
                        break;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "ComboElement")
                return;
            if (owner == BulletOwner.Character)
            {
                // Da utilizzare il behaviour che viene iniettato dall'arma al momento dell'instanziazione
                IDamageable damageable = other.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    DoDamage(damageable);
                    ApplyElementalEffect(other.GetComponent<Enemy>());
                }
                Destroy(gameObject); 
            }
            else
            {
                if (other.GetComponent<Enemy>() == null)
                {
                    DoDamage(other.GetComponent<IDamageable>());
                    Destroy(gameObject);
                }
            }
        }
    }

    public enum BulletOwner
    {
        Character,
        Enemy
    }
}