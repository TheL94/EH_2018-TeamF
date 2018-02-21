using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyBehaviourMelee : IEnemyBehaviour
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
            _enemy.data.Life -= _damage;         
        }

        public virtual void DoDeath()
        {
            // Azioni da compiere alla morte
        }
    }
}