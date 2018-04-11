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
            target.SetParalisys(true);
            //_enemy.AI_Enemy.CurrentState = _enemy.Data.ParalizedState;
        }

        public void DoStopEffect()
        {
            target.SetParalisys(false);
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