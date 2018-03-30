using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;
using System.Linq;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_AttackTargetRanged")]
    public class Enemy_AttackTargetRanged : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            AttackTargetMelee((_controller as AI_Enemy).Enemy);
            return true;
        }

        void AttackTargetMelee(Enemy _enemy)
        {
            BulletData bulletData = _enemy.Data.BulletData;
            Transform transF = FindShootingPoint(_enemy);
            if(transF != null)
            {
                Bullet bullet = Instantiate(bulletData.BulletContainerPrefab, transF.position, transF.rotation, null).AddComponent<Bullet>();

                if (bulletData.BulletGraphicPrefab != null)
                    Instantiate(bulletData.BulletGraphicPrefab, bullet.transform.position, bullet.transform.rotation, bullet.transform);
                if (bulletData.BulletTrailPrefab != null)
                    Instantiate(bulletData.BulletTrailPrefab, bullet.transform.position, bullet.transform.rotation, bullet.transform);

                ElementalAmmo ammo = new ElementalAmmo() { AmmoType = ElementalType.None, Damage = _enemy.Data.RangedDamage };
                BulletOwner owner;
                if (_enemy.IsCharmed)
                    owner = BulletOwner.EnemyChamed;
                else
                    owner = BulletOwner.Enemy;

                bullet.Init(ammo, _enemy.Data.BulletSpeed, owner, 2.0f);
            }
        }

        Transform FindShootingPoint(Enemy _enemy)
        {
            return _enemy.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.tag == "ShootingPoint");
        }
    }
}
