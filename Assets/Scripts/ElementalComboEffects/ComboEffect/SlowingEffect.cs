using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class SlowingEffect : IElementalEffectBehaviour
    {
        Enemy enemy;
        ElementalEffectData elementalData;
        float previousMovementSpeed;

        public void DoInit(Enemy _enemy, ElementalEffectData _data)
        {
            enemy = _enemy;
            elementalData = _data;
            previousMovementSpeed = enemy.MovementSpeed;
            enemy.MovementSpeed = _data.EffectValue;
        }

        public void DoStopEffect()
        {
            enemy.MovementSpeed = previousMovementSpeed;
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