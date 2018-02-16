using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF {
    public class ElementalBehaviourWater : IBulletEffectBehaviour {

        float slowdown = 3;
        Enemy enemy;
        public void DoInit(Enemy _enemy, float _value)
        {
            enemy = _enemy;
            slowdown = _value;
        }

        public void DoEffect()
        {
            enemy.MovementSpeed -= slowdown;
        }

        public void DoStopEffect() { }
    }
}