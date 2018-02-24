using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class SlowingEffect : IElementalEffectBehaviour
    {
        Enemy enemy;
        ElementalEffectData elementalData;
        float startMovementSpeed;

        public void DoInit(Enemy _enemy, ElementalEffectData _data)
        {
            enemy = _enemy;
            elementalData = _data;
            startMovementSpeed = enemy.MovementSpeed;
            enemy.SetPercentageOfMovementSpeed(_data.EffectValue);
        }

        public void DoStopEffect()
        {
            enemy.MovementSpeed = startMovementSpeed;
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