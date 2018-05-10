using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class IncreaseDamage : ElementalComboBase
    {
        public ElementalEffectData EffectData;

        protected override void OnEnterCollider(Collider other)
        {
            IEffectable _target = other.GetComponent<IEffectable>();
            EffectController _effect = other.GetComponent<EffectController>();
            if (_effect != null && _target != null)
            {
                _effect.InitEffect(new IncreaseDamageEffect(), _target, EffectData, true);
            }
        }
    }
}