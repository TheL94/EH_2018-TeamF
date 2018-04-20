using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamF
{
    public class FireExplosion : ElementalComboBase
    {
        public float Damage;
        protected override void DoInit()
        {
            base.DoInit();
            transform.DOScale(3, 2);
        }

        protected override void OnEnteringCollider(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(Damage, ElementalType.Fire);
            }
        }
    }
}