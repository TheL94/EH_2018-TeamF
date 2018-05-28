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
            return true;
        }

        void MeleeAttack(Enemy _enemy)
        {
            _enemy.Target.TakeDamage(_enemy.Data.MeleeDamage);
            GameManager.I.AudioMng.PlaySound(Clips.EnemyAttack);
        }

        void RangedAttack(Enemy _enemy)
        {
            BulletData bulletData = _enemy.Data.BulletData;
            Transform transF = _enemy.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.tag == "ShootingPoint");

            if (transF != null)
            {
                Bullet bullet = Instantiate(bulletData.BulletContainerPrefab, transF.position, transF.rotation, null).GetComponent<Bullet>();

                if (bulletData.BulletGraphicPrefab != null)
                    Instantiate(bulletData.BulletGraphicPrefab, bullet.transform.position, bullet.transform.rotation, bullet.transform);
                if (bulletData.BulletTrailPrefab != null)
                    Instantiate(bulletData.BulletTrailPrefab, bullet.transform.position, bullet.transform.rotation, bullet.transform);

                bullet.Init(bulletData.ElementalAmmo, _enemy.Data.BulletSpeed, _enemy, _enemy.Data.BulletRange);
                GameManager.I.AudioMng.PlaySound(Clips.EnemyAttack);
            }
            else
                Debug.LogError("No ShootingPoint Found On " + _enemy.Data.EnemyType + " !");
        }
    }
}
