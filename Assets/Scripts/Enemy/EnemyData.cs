using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public EnemyType EnemyType;
        public ElementalType ElementalType;
        public GameObject ModelPrefab;

        public float Life;
        public float Speed;

        public int Damage;
        public float DamageRange;
        public float DamageRate;

        public float EnemyValue;
    }
}