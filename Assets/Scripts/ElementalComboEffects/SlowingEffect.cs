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
            if (!target.IsSlowed)
            {
                target.IsSlowed = true;
                previousMovementSpeed = target.MovementSpeed;
                target.MovementSpeed = target.MovementSpeed - (target.MovementSpeed * _data.EffectValue) / 100; 
            }

            (target as MonoBehaviour).GetComponentInChildren<BlinkController>().SlowedBlink(elementalData.TimeOfEffect);
        }

        public void DoStopEffect()
        {
            if(target != null)
            {
                target.MovementSpeed = previousMovementSpeed;
                target.IsSlowed = false;
                
            }
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