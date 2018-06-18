using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamF
{
    public class FireExplosion : ElementalComboBase
    {
        public float Damage;
        protected override void OnInit()
        {
            transform.DOScale(3, 2);
            GameManager.I.AudioMng.PlaySound(Clips.ComboFireExplosion);
        }

        protected override void OnEnterCollider(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if(damageable != null && other.GetComponent<Character>() == null)
            {
                damageable.TakeDamage(Damage, ElementalType.Fire);
            }
        }
    }
}