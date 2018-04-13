using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class SlowingEffect : IElementalEffectBehaviour
    {
        IEffectable target;
        ElementalEffectData elementalData;
        float previousMovementSpeed;

        public void DoInit(IEffectable _target, ElementalEffectData _data)
        {
            target = _target;
            elementalData = _data;
            previousMovementSpeed = target.MovementSpeed;
            target.MovementSpeed = _data.EffectValue;
        }

        public void DoStopEffect()
        {
            target.MovementSpeed = previousMovementSpeed;
        }

        public bool DoUpdate()
        {
            elementalData.TimeOfEffect -= Time.deltaTime;
            if (elementalData.TimeOfEffect <= 0)
                return true;

            return false;
        }
    }
}