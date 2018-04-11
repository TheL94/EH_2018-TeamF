using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class CharmEffect : IElementalEffectBehaviour
    {
        IEffectable target;
        ElementalEffectData elementalData;

        public void DoInit(IEffectable _target, ElementalEffectData _data)
        {
            target = _target;
            elementalData = _data;
            (target as ICharmable).IsCharmed = true;
        }

        public void DoStopEffect()
        {
            (target as ICharmable).IsCharmed = false;
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
    }
}