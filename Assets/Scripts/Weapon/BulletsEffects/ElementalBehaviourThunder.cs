using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;


namespace TeamF
{
    public class ElementalBehaviourThunder : IBulletEffectBehaviour
    {
        Character enemyTarget;
        Enemy enemy;

        public void DoInit(Enemy _enemy, float _value)
        {
            enemy = _enemy;
            enemyTarget = enemy.target;
        }

        public void DoEffect()
        {
            enemy.target = null;
        }

        public void DoStopEffect()
        {
            enemy.target = enemyTarget;
        }

    }
}