using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;
using System.Linq;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_AttackTarget")]
    public class Enemy_AttackTarget : AI_Action
    {
        public AttackType AttackType;

        protected override bool Act(AI_Controller _controller)
        {
            switch (AttackType)
            {
                case AttackType.Melee:
                    MeleeAttack((_controller as AI_Enemy).Enemy);                   
                    break;
                case AttackType.Ranged:
                    RangedAttack((_controller as AI_Enemy).Enemy);
                    break;
            }
            Debug.Log((_controller as AI_Enemy).Enemy.Data.EnemyType + " / " + AttackType);
            return true;
        }

        void MeleeAttack(Enemy _enemy)
        {
            _enemy.Target.TakeDamage(_enemy.Data.MeleeDamage);
        }

        void RangedAttack(Enemy _enemy)
        {
            BulletData bulletData = _enemy.Data.BulletData;
            Transform transF = _enemy.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.tag == "ShootingPoint");
            if (transF != null)
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

                bullet.Init(ammo, _enemy.Data.BulletSpeed, owner, _enemy.Data.BulletLifeTime);
            }
            else
                Debug.LogWarning("No ShootingPoint Found !");
        }
    }
}
