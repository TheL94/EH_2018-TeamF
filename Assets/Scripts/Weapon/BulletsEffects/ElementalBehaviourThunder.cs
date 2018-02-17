using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


namespace TeamF
{
    public class ElementalBehaviourThunder : IBulletEffectBehaviour
    {
        Character enemyTarget;
        Enemy enemy;
        ElementalEffectData elementalData;

        public void DoInit(Enemy _enemy, ElementalEffectData _data)
        {
            enemy = _enemy;
            elementalData = _data;
            enemyTarget = enemy.target;
            enemy.target = null;
        }

        public bool DoUpdate()
        {
            elementalData.TimeOfEffect -= Time.deltaTime;
            if(elementalData.TimeOfEffect <= 0)
            {
                enemy.target = enemyTarget;
                return true;
            }
            return false;
        }

        public void DoStopEffect()
        {
            enemy.target = enemyTarget;
        }

    }
}