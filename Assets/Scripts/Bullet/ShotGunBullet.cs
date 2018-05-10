using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ShotGunBullet : Bullet
    {

        public override void Init(ElementalAmmo _currentAmmo, float _speed, IShooter _owner, float _bulletLife)
        {
            base.Init(_currentAmmo, _speed, _owner, _bulletLife);
        }

        private void Update()
        {
            Vector3 scale = transform.localScale;
            scale.x += Time.deltaTime * 50;
            transform.localScale = scale;
        }

        protected override void OnTrigger(Collider other)
        {
            if (other.tag == "Terrain")
                Destroy(gameObject);

            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                DoDamage(damageable);
                ApplyElementalEffect(other.GetComponent<Enemy>());
            }
        }
    }
}