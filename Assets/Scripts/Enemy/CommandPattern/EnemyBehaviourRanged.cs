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
        ElementalAmmo ammo;

        public void DoInit(Enemy _myEnemy)
        {
            myEnemy = _myEnemy;
            shootingPoint = FindShootingPoint("ShootingPoint");
            ammo = new ElementalAmmo();
            ammo.AmmoType = ElementalType.None;
            ammo.Damage = myEnemy.data.Damage;
        }

        public virtual void DoAttack()
        {
            Bullet bull = GameObject.Instantiate(Resources.Load("Bullet") as GameObject, shootingPoint.position, shootingPoint.rotation).GetComponent<Bullet>();
            bull.Init(ammo, 1f, BulletOwner.Enemy); 
        }

        public virtual void DoTakeDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            _enemy.data.Life -= _damage;          
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