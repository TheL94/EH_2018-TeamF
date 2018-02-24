using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class IncreaseDamageEffect : IElementalEffectBehaviour
    {
        Enemy enemy;
        ElementalEffectData elementalData;
        float startMultiplyer;

        public void DoInit(Enemy _enemy, ElementalEffectData _data)
        {
            enemy = _enemy;
            elementalData = _data;
            startMultiplyer = enemy.DamageMultiplier;
            enemy.DamageMultiplier = _data.EffectValue;
        }

        public void DoStopEffect()
        {
            enemy.DamageMultiplier = startMultiplyer;
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