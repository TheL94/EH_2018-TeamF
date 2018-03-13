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
            BulletEffect effect = other.GetComponent<BulletEffect>();
            if(effect != null)
            {
                effect.InitEffect(new ParalizeEffect(), other.GetComponent<Enemy>(), EffectData, true);
            }
        }
    }
}