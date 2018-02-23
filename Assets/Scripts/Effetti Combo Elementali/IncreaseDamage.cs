using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class IncreaseDamage : ElementalComboBase
    {
        public float MultiplierPercentage;
        protected override void OnEnteringCollider(Collider other)
        {
            IDamageable enemy = other.GetComponent<IDamageable>();
            if(enemy != null)
            {
                enemy.DamageMultiplier = MultiplierPercentage;
            }
        }

        protected override void OnExitCollider(Collider other)
        {
            IDamageable enemy = other.GetComponent<IDamageable>();
            if (enemy != null)
            {
                enemy.DamageMultiplier = 1;
            }
        }
    }
}