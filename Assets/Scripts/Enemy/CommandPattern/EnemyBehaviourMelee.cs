using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyBehaviourMelee : IEnemyBehaviour
    {
        public  Enemy myEnemy;
        float multiplier = 1;
        public float Multiplier { get; set; }

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
            _damage += (Multiplier * _damage) / 100;
            _enemy.Life -= _damage;         
        }

        public virtual void DoDeath(ElementalType _bulletType)
        {
            // Azioni da compiere alla morte
        }
    }
}