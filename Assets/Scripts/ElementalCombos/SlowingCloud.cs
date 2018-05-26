using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class SlowingCloud : ElementalComboBase
    {
        public float ReductionPercentage;
        float enemySpeed;

        protected override void OnInit()
        {
            GameManager.I.AudioMng.PlaySound(Clips.ComboSlowingCloud);
        }

        protected override void OnEnterCollider(Collider other)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemySpeed = enemy.MovementSpeed;
                enemy.MovementSpeed -= (enemySpeed * ReductionPercentage) / 100;
            }
        }

        protected override void OnExitCollider(Collider other)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.MovementSpeed = enemySpeed;
            }
        }
    }
}