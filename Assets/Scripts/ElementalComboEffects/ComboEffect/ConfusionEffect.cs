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
            if(Enemy.EnemyConfusion != null)
                Enemy.EnemyConfusion(enemy);
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

            if(enemy.Target != null)
            {
                if (Enemy.EnemyConfusion != null)
                    Enemy.EnemyConfusion(enemy);
            }

            return false;
        }
    }
}