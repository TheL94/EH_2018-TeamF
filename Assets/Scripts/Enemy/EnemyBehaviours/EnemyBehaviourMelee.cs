using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyBehaviourMelee : IEnemyBehaviour
    {
        public  Enemy myEnemy;
        float multiplier = 1;
        public float Multiplier { get { return multiplier; } set { multiplier = value; } }

        public virtual void DoInit(Enemy _myEnemy)
        {
            myEnemy = _myEnemy;
        }

        public virtual void DoAttack()
        {
            myEnemy.Target.TakeDamage(myEnemy.Data.Damage);
        }

        public virtual float CalulateDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            _damage += (Multiplier * _damage) / 100;
            return _damage; 
        }

        public virtual void DoDeath(ElementalType _bulletType)
        {
            // Azioni da compiere alla morte
        }
    }
}