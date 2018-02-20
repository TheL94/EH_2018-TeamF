using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public ElementalType EnemyType;
        public GameObject ModelPrefab;

        public float Life;

        public int Damage;
        public float DamageRange;
        public float DamageRate;

        public float EnemyValue;
    }
}