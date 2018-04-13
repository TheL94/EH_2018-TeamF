using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF {
    public class SetOnFireEffect : IElementalEffectBehaviour {

        IEffectable target;
        ElementalEffectData elementalData;
        float timer;

        public void DoInit(IEffectable _target, ElementalEffectData _data)
        {
            target = _target;
            elementalData = _data;
            timer = _data.TimeFraction;
        }

        public bool DoUpdate()
        {
            elementalData.TimeOfEffect -= Time.deltaTime;
            if (elementalData.TimeOfEffect <= timer)
            {
                if (elementalData.EffectValue > 0)
                {
                    target.TakeDamage(elementalData.EffectValue, ElementalType.Fire);
                    elementalData.EffectValue -= 1;
                    if (elementalData.EffectValue < 0)
                        elementalData.EffectValue = 0;
                }
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