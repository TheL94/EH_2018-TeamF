using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ParalyzingCloud : ElementalComboBase
    {
        public ElementalEffectData EffectData;

        protected override void OnInit()
        {
            GameManager.I.AudioMng.PlaySound(Clips.ComboParalyzingCloud);
        }

        protected override void OnEnterCollider(Collider other)
        {
            Enemy _enemy = other.GetComponent<Enemy>();
            EffectController effect = other.GetComponent<EffectController>();
            if(effect != null && _enemy != null)
                effect.InitEffect(new ParalyzeEffect(), other.GetComponent<IEffectable>(), EffectData, true);
        }
    }
}