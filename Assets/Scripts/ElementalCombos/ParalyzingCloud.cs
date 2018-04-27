using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ParalyzingCloud : ElementalComboBase
    {
        public ElementalEffectData EffectData;        

        protected override void OnEnteringCollider(Collider other)
        {
            EffectController effect = other.GetComponent<EffectController>();
            if(effect != null)
            {
                effect.InitEffect(new ParalyzeEffect(), other.GetComponent<IEffectable>(), EffectData, true);
            }
        }
    }
}