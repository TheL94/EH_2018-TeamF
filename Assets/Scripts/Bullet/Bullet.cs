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
        float damagePercentage;

        void FixedUpdate()
        {
            Move();
        }

        #region API

        public virtual void Init(ElementalAmmo _currentAmmo, float _speed, BulletOwner _owner, float _bulletLife)
        {
            ammo = _currentAmmo;
            Speed = _speed;
            owner = _owner;
            trail = GetComponentInChildren<TrailRenderer>();
            rend = GetComponentInChildren<MeshRenderer>();
            SetBulletColors(_currentAmmo.AmmoType);

            Destroy(gameObject,_bulletLife);
        }

        public virtual void Init(ElementalAmmo _currentAmmo, float _speed, BulletOwner _owner, float _bulletLife, float _damagePercentage)
        {
            damagePercentage = _damagePercentage;
            Init(_currentAmmo, _speed, _owner, _bulletLife);
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

        protected void DoDamage(IDamageable _damageable)
        {
            if (_damageable != null)
            {
                float damage = ammo.Damage + (ammo.Damage * damagePercentage / 100);
                _damageable.TakeDamage(damage, ammo.AmmoType);              
            }
        }

        protected void ApplyElementalEffect(IEffectable _effectable)
        {
            if (_effectable != null)
            {
                EffectController _effect = (_effectable as MonoBehaviour).GetComponent<EffectController>();
                if (_effect != null)
                {
                    switch (ammo.AmmoType)
                    {
                        case ElementalType.Fire:
                            _effect.InitEffect(new SetOnFireEffect(), _effectable, ammo.Data);
                            break;
                        case ElementalType.Water:
                            _effect.InitEffect(new SlowingEffect(), _effectable, ammo.Data);
                            break;
                        case ElementalType.Poison:
                            _effect.InitEffect(new PoisonedEffect(), _effectable, ammo.Data);
                            break;
                        case ElementalType.Thunder:
                            _effect.InitEffect(new ParalizeEffect(), _effectable, ammo.Data);
                            break;
                        case ElementalType.None:
                            break;
                    } 
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "ComboElement")
                return;
            OnTrigger(other);
        }

        protected virtual void OnTrigger(Collider other)
        {
            IDamageable damageable;
            switch (owner)
            {
                case BulletOwner.Character:
                    damageable = other.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        DoDamage(damageable);
                        ApplyElementalEffect(other.GetComponent<IEffectable>());
                    }
                    Destroy(gameObject);
                    break;
                case BulletOwner.Enemy:
                    if (other.GetComponent<Enemy>() == null)
                    {
                        damageable = other.GetComponent<IDamageable>();
                        if (damageable != null)
                        {
                            DoDamage(damageable);
                            ApplyElementalEffect(other.GetComponent<IEffectable>());
                        }
                        Destroy(gameObject);
                    }
                    break;
                case BulletOwner.EnemyChamed:
                    damageable = other.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        DoDamage(damageable);
                        ApplyElementalEffect(other.GetComponent<IEffectable>());
                    }
                    Destroy(gameObject);
                    break;
            }
        }
    }

    public enum BulletOwner
    {
        Character,
        Enemy,
        EnemyChamed
    }
}