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
            IEnemyBehaviour enemy = other.GetComponent<IEnemyBehaviour>();
            if(enemy != null)
            {
                enemy.Multiplier = MultiplierPercentage;
            }
        }

        protected override void OnExitCollider(Collider other)
        {
            IEnemyBehaviour enemy = other.GetComponent<IEnemyBehaviour>();
            if (enemy != null)
            {
                enemy.Multiplier = 1;
            }
        }
    }
}