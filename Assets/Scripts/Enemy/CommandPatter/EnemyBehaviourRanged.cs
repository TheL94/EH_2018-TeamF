using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyBehaviourRanged : IEnemyBehaviour
    {
        Enemy myEnemy;

        Transform shootPoint;
        public void DoInit(Enemy _myEnemy)
        {
            myEnemy = _myEnemy;
        }

        public virtual void DoAttack()
        {
            // Attacco base del nemico
        }

        public virtual void DoTakeDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            //Take damage base
            _enemy.data.Life -= _damage;
            
        }

        public virtual void DoDeath()
        {
            // Azioni da compiere alla morte
        }
    }
}