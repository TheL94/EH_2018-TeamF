using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    [CreateAssetMenu(fileName = "EnemyFireData", menuName = "Enemy/EnemyFireData")]
    public class EnemyFireData : EnemyGenericData
    {
        [Header("Fire Specific Parameters")]
        public float RangedAttackDuration;
        public float MeleeConsecutiveAttacks;
    }
}