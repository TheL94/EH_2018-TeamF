﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ParalizeEffect : IElementalEffectBehaviour
    {
        ElementalEffectData elementalData;

        public void DoInit(Enemy _enemy, ElementalEffectData _data)
        {
            elementalData = _data;
            _enemy.AI_Enemy.CurrentState = _enemy.Data.ParalizedState;
        }

        public void DoStopEffect() { }

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