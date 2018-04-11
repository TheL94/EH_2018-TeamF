using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF
{
    [CreateAssetMenu(fileName = "EnemyGenericData", menuName = "Enemy/EnemyGenericData")]
    public class EnemyGenericData : ScriptableObject
    {
        [Header("AI States")]
        public AI_State InitialState;
        public AI_State CharmedState;
        public AI_State ParalizedState;
        public AI_State DamageState;

        [Header("AI Parameters")]
        public float StoppingDistance;
        public float AimApproximationAngle;
        public float RangeOffset;

        [Header("Object Constuction")]
        public GameObject ContainerPrefab;
        public GameObject GraphicPrefab;

        [Header("Enemy Parameters")]
        public EnemyType EnemyType;
        public ElementalType ElementalType
        {
            get
            {
                switch (EnemyType)
                {
                    case EnemyType.Melee:
                        return ElementalType.None;
                    case EnemyType.Ranged:
                        return ElementalType.None;
                    case EnemyType.Fire:
                        return ElementalType.Fire;
                    case EnemyType.Water:
                        return ElementalType.Water;
                    case EnemyType.Poison:
                        return ElementalType.Poison;
                    case EnemyType.Thunder:
                        return ElementalType.Thunder;
                }
                return ElementalType.None;
            }
        }
        public float Life;
        public float Speed;
        public float AimTime;
        public float EnemyValue;

        [Header("Melee Attack Parameters")]
        public float MeleeDamage;
        public float MeleeDamageRange;
        public float MeleeDamageRate;

        [Header("Ranged Attack Parameters")]
        public float RangedDamage;
        public float RangedDamageRange;
        public float RangedDamageRate;

        [Header("Bullet Parameters")]
        public BulletData BulletData;
        public float BulletSpeed;
        public float BulletLifeTime;
    }
}