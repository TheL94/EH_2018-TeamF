using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ParalizeEffect : IElementalEffectBehaviour
    {
        ElementalEffectData elementalData;
        IEffectable target;

        public void DoInit(IEffectable _target, ElementalEffectData _data)
        {
            target = _target;
            elementalData = _data;
            target.IsParalized = true;
        }

        public void DoStopEffect()
        {
            target.IsParalized = false;
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