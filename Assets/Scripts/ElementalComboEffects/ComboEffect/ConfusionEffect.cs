using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ConfusionEffect : IElementalEffectBehaviour
    {
        Enemy enemy;
        ElementalEffectData elementalData;
        IDamageable previousTarget;

        public void DoInit(Enemy _enemy, ElementalEffectData _data)
        {
            enemy = _enemy;
            elementalData = _data;
            previousTarget = enemy.Target;
            enemy.Target = null;
        }

        public void DoStopEffect()
        {
            enemy.Target = previousTarget;
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