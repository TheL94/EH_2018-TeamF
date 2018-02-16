using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ElementalBehaviourPoison : IBulletEffectBehaviour
    {
        float damage;
        Enemy enemy;

        public void DoInit(Enemy _enemy, float _value)
        {
            enemy = _enemy;
            damage = _value;
        }

        public void DoEffect()
        {
            if (damage > 0)
            {
                enemy.TakeDamage(damage, ElementalType.Poison);
            }
        }


        public void DoStopEffect() { }
    }
}