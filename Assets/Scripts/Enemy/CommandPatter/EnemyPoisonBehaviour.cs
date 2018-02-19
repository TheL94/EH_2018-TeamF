using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyPoisonBehaviour : EnemyBehaviourBase
    {
        public override void DoAttack()
        {
            base.DoAttack();
        }

        public override void TakeDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            if(_type == ElementalType.Fire)
                _damage *= 1.5f;
            if (_type == ElementalType.Poison)
                _damage = 0;
            base.TakeDamage(_enemy, _damage, _type);
        }
    }
}