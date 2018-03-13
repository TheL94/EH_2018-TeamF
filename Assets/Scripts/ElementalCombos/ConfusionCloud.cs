using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ConfusionCloud : ElementalComboBase
    {
        public ElementalEffectData EffectData;

        protected override void OnEnteringCollider(Collider other)
        {
            Enemy _enemy = other.GetComponent<Enemy>();
            EffectController _effect = other.GetComponent<EffectController>();
            if (_effect != null && _enemy != null)
            {
                _effect.InitEffect(new ConfusionEffect(), _enemy, EffectData, true);
            }
        }

    }
}