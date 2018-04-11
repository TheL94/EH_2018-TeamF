using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


namespace TeamF
{
    public class BulletEffectThunder : IElementalEffectBehaviour
    {
        IEffectable target;
        ElementalEffectData elementalData;

        public void DoInit(IEffectable _target, ElementalEffectData _data)
        {
            target = _target;
            elementalData = _data;
            target.IsParalized = true;
        }

        public bool DoUpdate()
        {
            elementalData.TimeOfEffect -= Time.deltaTime;
            if(elementalData.TimeOfEffect <= 0)
                return true;

            return false;
        }

        public void DoStopEffect()
        {
            target.IsParalized = false;
        }
    }
}