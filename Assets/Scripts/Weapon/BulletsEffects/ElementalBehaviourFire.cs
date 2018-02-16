using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF {
    public class ElementalBehaviourFire : IBulletEffectBehaviour {

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
                enemy.TakeDamage(damage, ElementalType.Fire);
                damage -= 1;
                if (damage < 0)
                    damage = 0; 
            }
        }

        public void DoStopEffect() { }
    }
}