using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class PoisonedEffect : IElementalEffectBehaviour
    {
        IEffectable target;
        ElementalEffectData elementalData;
        float timer;

        public void DoInit(IEffectable _target, ElementalEffectData _data)
        {
            target = _target;
            elementalData = _data;
            timer = elementalData.TimeFraction;
        }

        public bool DoUpdate()
        {
            elementalData.TimeOfEffect -= Time.deltaTime;
            if (elementalData.TimeOfEffect <= timer)
            {
                if (elementalData.EffectValue > 0)
                    target.TakeDamage(elementalData.EffectValue, ElementalType.Poison);

                timer -= elementalData.TimeFraction;
            }
            if (elementalData.TimeOfEffect <= 0)
                return true;
            else
                return false;
        }


        public void DoStopEffect() { }
    }
}