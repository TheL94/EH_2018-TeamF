using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyThunderBehaviour : EnemyBehaviourBase
    {
        public override void DoInit()
        {
            base.DoInit();
            // Istanzia il modello del nemico tuono
        }

        public override void DoAttack()
        {
            base.DoAttack();
        }

        public override void TakeDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            if (_type == ElementalType.Poison)
                _damage *= 1.5f;
            if (_type == ElementalType.Thunder)
                _damage = 0;
            base.TakeDamage(_enemy, _damage, _type);
        }
    }
}