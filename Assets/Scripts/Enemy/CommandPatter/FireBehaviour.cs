using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class FireBehaviour : EnemyBehaviourBase
    {
        public override void DoAttack()
        {
            base.DoAttack();
        }

        public override void TakeDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            if (_type == ElementalType.Water)
                _damage *= 1.5f;
            if (_type == ElementalType.Fire)
                _damage = 0;
            base.TakeDamage(_enemy, _damage, _type);
        }
    }
}