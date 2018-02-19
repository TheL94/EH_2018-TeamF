using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyBehaviourBase : IEnemyBehaviour
    {
        public virtual void DoInit()
        {
            
        }

        public virtual void DoAttack()
        {
            // Attacco base del nemico
        }

        public virtual void DoTakeDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            //Take damage base
            _enemy.Life -= _damage;
            
        }

        public virtual void DoDeath()
        {
            // Azzioni da compiere alla morte
        }
    }
}