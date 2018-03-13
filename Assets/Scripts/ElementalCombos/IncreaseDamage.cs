using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class IncreaseDamage : ElementalComboBase
    {
        public ElementalEffectData EffectData;

        protected override void OnEnteringCollider(Collider other)
        {
            Enemy _enemy = other.GetComponent<Enemy>();
            BulletEffect _effect = other.GetComponent<BulletEffect>();
            if (_effect != null && _enemy != null)
            {
                _effect.InitEffect(new IncreaseDamageEffect(), _enemy, EffectData, true);
            }
        }
    }
}