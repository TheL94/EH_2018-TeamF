using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ConfusionCloud : ElementalComboBase
    {
        public ElementalEffectData EffectData;

        protected override void OnInit()
        {
            GameManager.I.AudioMng.PlaySound(Clips.ComboConfusionCloud);
        }

        protected override void OnEnterCollider(Collider other)
        {
            IEffectable _target = other.GetComponent<IEffectable>();
            EffectController _effect = other.GetComponent<EffectController>();
            if (_effect != null && _target != null && other.GetComponent<Character>() == null)
            {
                _effect.InitEffect(new CharmEffect(), _target, EffectData, true);
            }
        }

    }
}