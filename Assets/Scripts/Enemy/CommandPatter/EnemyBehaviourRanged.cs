using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TeamF
{
    public class EnemyBehaviourRanged : IEnemyBehaviour
    {
        Enemy myEnemy;

        Transform shootingPoint;

        public void DoInit(Enemy _myEnemy)
        {
            myEnemy = _myEnemy;
            shootingPoint = FindShootingPoint("ShootingPoint");
        }

        public virtual void DoAttack()
        {
            //Bullet bull = GameObject.Instantiate(BulletPrefab, shootingPoint.position, shootingPoint.rotation).GetComponent<Bullet>();
            //bull.Init(_currentAmmo, BulletSpeed);
        }

        public virtual void DoTakeDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            //Take damage base
            _enemy.Life -= _damage;
            
        }

        public virtual void DoDeath()
        {
            // Azioni da compiere alla morte
        }

        Transform FindShootingPoint(string _tag)
        {
            return myEnemy.GetComponentsInChildren<Transform>().ToList().Where(c => c.tag == _tag).First();
        }
    }
}