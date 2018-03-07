using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TeamF
{
    public class EnemyBehaviourRanged : IEnemyBehaviour
    {
        public float Multiplier { get; set; }

        Enemy myEnemy;
        Transform shootingPoint;
        ElementalAmmo ammo;

        float multiplier = 1;

        RangedData rangedData { get { return myEnemy.Data as RangedData; } }

        public void DoInit(Enemy _myEnemy)
        {
            myEnemy = _myEnemy;
            shootingPoint = FindShootingPoint("ShootingPoint");
            ammo = new ElementalAmmo();
            ammo.AmmoType = ElementalType.None;
            ammo.Damage = myEnemy.Data.Damage;
        }

        public virtual void DoAttack()
        {      
            Bullet bull = GameObject.Instantiate(rangedData.BulletData.BulletContainerPrefab, shootingPoint.position, shootingPoint.rotation).GetComponent<Bullet>();

            if(rangedData.BulletData.BulletGraphicPrefab != null)
                GameObject.Instantiate(rangedData.BulletData.BulletGraphicPrefab, bull.transform.position, bull.transform.rotation, bull.transform).GetComponent<Bullet>();
            if (rangedData.BulletData.BulletTrailPrefab != null)
                GameObject.Instantiate(rangedData.BulletData.BulletTrailPrefab, bull.transform.position, bull.transform.rotation, bull.transform).GetComponent<Bullet>();

            bull.Init(ammo, rangedData.BulletSpeed, BulletOwner.Enemy); 
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

        Transform FindShootingPoint(string _tag)
        {
            return myEnemy.GetComponentsInChildren<Transform>().ToList().Where(c => c.tag == _tag).First();
        }
    }
}