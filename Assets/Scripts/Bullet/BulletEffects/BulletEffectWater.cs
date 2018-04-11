using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF {
    public class BulletEffectWater : IElementalEffectBehaviour {

        float initialSlowdown;
        IEffectable target;
        ElementalEffectData elementalData;

        public void DoInit(IEffectable _target, ElementalEffectData _data)
        {
            target = _target;
            elementalData = _data;
            initialSlowdown = target.MovementSpeed;
            target.MovementSpeed -= _data.EffectValue;
        }

        public bool DoUpdate()
        {
            elementalData.TimeOfEffect -= Time.deltaTime;
            if (elementalData.TimeOfEffect <= 0)
            {
                return true;
            }
            return false;
        }

        public void DoStopEffect()
        {
            target.MovementSpeed = initialSlowdown;
        }
    }
}