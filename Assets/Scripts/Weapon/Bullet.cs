using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Bullet : MonoBehaviour
    {
        public float BulletLife;
        TrailRenderer trail;
        MeshRenderer rend;
        BulletOwner owner;
        ElementalAmmo ammo;
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

        public void Init(ElementalAmmo _currentAmmo, float _speed, BulletOwner _owner)
        {
            ammo = _currentAmmo;
            Speed = _speed;
            owner = _owner;
            trail = GetComponentInChildren<TrailRenderer>();
            rend = GetComponentInChildren<MeshRenderer>();

            SetBulletColors(_currentAmmo.AmmoType);
        }

        #endregion

        void SetBulletColors(ElementalType _type)
        {
            switch (_type)
            {
                case ElementalType.None:
                    rend.material.color = Color.grey;
                    //trail.material.color = Color.grey;
                    //trail.material.SetColor("_EmissionColor", Color.grey);
                    break;
                case ElementalType.Fire:
                    rend.material.color = Color.red;
                    //trail.material.color = Color.red;
                    //trail.material.SetColor("_EmissionColor", Color.red);
                    break;
                case ElementalType.Water:
                    rend.material.color = Color.blue;
                    //trail.material.color = Color.blue;
                    //trail.material.SetColor("_EmissionColor", Color.blue);
                    break;
                case ElementalType.Poison:
                    rend.material.color = Color.green;
                    //trail.material.color = Color.green;
                    //trail.material.SetColor("_EmissionColor", Color.green);
                    break;
                case ElementalType.Thunder:
                    rend.material.color = Color.magenta;
                    //trail.material.color = Color.magenta;
                    //trail.material.SetColor("_EmissionColor", Color.magenta);
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
                ElementalEffect _effect = _enemy.GetComponent<ElementalEffect>();
                switch (ammo.AmmoType)
                {
                    case ElementalType.Fire:
                        _effect.InitEffect(new ElementalEffectFire(), _enemy, ammo.Data);
                        break;
                    case ElementalType.Water:
                        _effect.InitEffect(new ElementalEffectWater(), _enemy, ammo.Data);
                        break;
                    case ElementalType.Poison:
                        _effect.InitEffect(new ElementalEffectPoison(), _enemy, ammo.Data);
                        break;
                    case ElementalType.Thunder:
                        _effect.InitEffect(new ElementalEffectThunder(), _enemy, ammo.Data);
                        break;
                    case ElementalType.None:
                        break;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(owner == BulletOwner.Character)
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    DoDamage(damageable);
                    ApplyElementalEffect(other.GetComponent<Enemy>());
                    Destroy(gameObject); 
                }
            }
            else
            {
                if(other.GetComponent<Enemy>() == null)
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