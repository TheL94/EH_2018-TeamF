using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyBehaviourBase : IEnemyBehaviour
    {
        Enemy myEnemy;
        public virtual void DoInit(Enemy _myEnemy)
        {
            myEnemy = _myEnemy;
        }

        public virtual void DoAttack()
        {
            myEnemy.target.TakeDamage(myEnemy.data.Damage);
        }

        public virtual void DoTakeDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            //Take damage base
            _enemy.data.Life -= _damage;
            
        }

        public virtual void DoDeath()
        {
            // Azzioni da compiere alla morte
        }
    }
}