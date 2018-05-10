using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ParalyzingCloud : ElementalComboBase
    {
        public ElementalEffectData EffectData;        

        protected override void OnEnterCollider(Collider other)
        {
            EffectController effect = other.GetComponent<EffectController>();
            if(effect != null)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                    effect.InitEffect(new ParalyzeEffect(), enemy as IEffectable, EffectData, true);
            }
        }
    }
}