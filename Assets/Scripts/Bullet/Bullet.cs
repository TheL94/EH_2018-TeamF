using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Bullet : MonoBehaviour
    {
        public string FragGraphicID;

        TrailRenderer trail;
        MeshRenderer rend;
        IShooter owner;
        ElementalAmmo ammo;
        float Speed;
        float damagePercentage;
        bool hasHit;

        float range;
        Vector3 initialPosition;

        void Update()
        {
            if (!hasHit)
                Move();

            if (Vector3.Distance(initialPosition, transform.position) > range)
                Destroy(gameObject);
        }

        #region API
        public virtual void Init(ElementalAmmo _currentAmmo, float _speed, IShooter _owner, float _range)
        {
            ammo = _currentAmmo;
            Speed = _speed;
            owner = _owner;
            range = _range;
            trail = GetComponentInChildren<TrailRenderer>();
            rend = GetComponentInChildren<MeshRenderer>();
            SetBulletColors(_currentAmmo.AmmoType);

            initialPosition = transform.position;
        }

        public virtual void Init(ElementalAmmo _currentAmmo, float _speed, IShooter _owner, float _range, float _damagePercentage)
        {
            damagePercentage = _damagePercentage;
            Init(_currentAmmo, _speed, _owner, _range);
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
                            _effect.InitEffect(new ParalyzeEffect(), _effectable, ammo.Data);
                            break;
                        case ElementalType.None:
                            break;
                    } 
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "ComboElement" || other.tag == "ColliderToIgnore")
                return;
            OnTrigger(other);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        protected virtual void OnTrigger(Collider other)
        {
            //Vector3 closetPoint = other.ClosestPoint(transform.position);

            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable == null)
            {
                //DropFragParticle(closetPoint);
                Destroy(gameObject);
                return;
            }

            if ((damageable as IShooter) == owner)
                return;

            if (owner.GetType() == typeof(Enemy))
            {
                if((owner as Enemy).IsCharmed)
                {
                    // Enemy Charmed
                    DoDamage(damageable);
                    ApplyElementalEffect(other.GetComponent<IEffectable>());
                    Destroy(gameObject);
                    //DropFragParticle(closetPoint);
                    return;
                }

                if (other.GetComponent<Enemy>() == null)
                {
                    // Enemy
                    DoDamage(damageable);
                    ApplyElementalEffect(other.GetComponent<IEffectable>());
                    Destroy(gameObject);
                    //DropFragParticle(closetPoint);
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                // Character
                DoDamage(damageable);
                ApplyElementalEffect(other.GetComponent<IEffectable>());
                Destroy(gameObject);
                //DropFragParticle(closetPoint);
                return;
            }
        }

        void DropFragParticle(Vector3 _position)
        {
            hasHit = true;
            GetComponent<Collider>().enabled = false;

            GameObject fragParticle = GameManager.I.PoolMng.GetObject(FragGraphicID);
            fragParticle.transform.position = _position;
            StartCoroutine(WaitForParticle(fragParticle));
        }

        IEnumerator WaitForParticle(GameObject _obj)
        {
            ParticleSystem particle = _obj.GetComponentInChildren<ParticleSystem>();

            while (particle.isPlaying)
                yield return new WaitForEndOfFrame();

            particle.gameObject.SetActive(false);
            GameManager.I.PoolMng.UpdatePool(FragGraphicID);
            Destroy(gameObject, 0.1f);
        }
    }
}