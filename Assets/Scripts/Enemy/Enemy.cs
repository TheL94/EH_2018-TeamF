using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Framework.AI;
using TeamF.AI;
using DG.Tweening;

namespace TeamF
{
    [RequireComponent(typeof(NavMeshAgent), typeof(AI_Enemy))]
    public class Enemy : MonoBehaviour, IDamageable, IParalyzable
    {
        public EnemyData Data { get; private set; }
        public string ID { get; private set; }
        public float MovementSpeed
        {
            get { return Agent.speed; }
            set
            {
                Data.Speed += (Data.Speed * value) / 100;
                Agent.speed = Data.Speed;

            }
        }

        MeshRenderer render;
        AI_Enemy ai_Enemy;

        #region API
        public void Init(IDamageable _target, EnemyData _data, AI_State _initalState, string _id)
        {
            Target = _target;
            Data = _data;
            ID = _id;
            Life = Data.Life;
            
            Instantiate(Data.ModelPrefab, transform.position, transform.rotation, transform);

            Agent = GetComponent<NavMeshAgent>();
            ai_Enemy = GetComponent<AI_Enemy>();
            render = GetComponentInChildren<MeshRenderer>();
            animator = GetComponentInChildren<Animator>();

            CurrentBehaviour = DeterminateBehaviourFromType(Data);
            CurrentBehaviour.DoInit(this);

            Agent.speed = Data.Speed;
            Agent.stoppingDistance = Data.DamageRange;
            Agent.SetDestination(Target.Position);

            ai_Enemy.InitialDefaultState = _initalState;
            ai_Enemy.IsActive = true;
        }
        #endregion

        #region Nav Mesh Agent
        public NavMeshAgent Agent { get; private set; }
        public IDamageable Target { get; set; }
        #endregion

        #region Enemy Behaviour
        public IEnemyBehaviour CurrentBehaviour { get; private set; }

        IEnemyBehaviour DeterminateBehaviourFromType(EnemyData _data)
        {
            switch (_data.EnemyType)
            {
                case EnemyType.Melee:
                    return new EnemyBehaviourMelee();
                case EnemyType.Ranged:
                    return new EnemyBehaviourRanged();
                case EnemyType.Fire:
                    return new EnemyFireBehaviour();
                case EnemyType.Water:
                    return new EnemyWaterBehaviour();
                case EnemyType.Poison:
                    return new EnemyPoisonBehaviour();
                case EnemyType.Thunder:
                    return new EnemyThunderBehaviour();
            }                            
            return null;
        }
        #endregion

        #region IDamageable
        public float Life { get; private set; }

        public Vector3 Position { get { return transform.position; } }

        float _damagePercentage = 100;
        public float DamagePercentage
        {
            get { return _damagePercentage; }
            set { _damagePercentage = value; }
        }

        public void TakeDamage(float _damage, ElementalType _type = ElementalType.None)
        {
            _damage = (_damage * DamagePercentage) / 100;
            Life -= CurrentBehaviour.CalulateDamage(this, _damage, _type);

            render.material.DOColor(Color.white, .1f).OnComplete(() => { render.material.DORewind(); });

            if (Life <= 0)
            {
                ai_Enemy.IsActive = false;
                CurrentBehaviour.DoDeath(_type);
                if(EnemyDeath != null)
                    EnemyDeath(this);
            }
        }
        #endregion

        #region IParalyzable
        /// <summary>
        /// Chiamata dalla combo elementale paralizzante
        /// </summary>
        public bool IsParalized { get; set; }
        #endregion

        #region Enemy Delegate
        public delegate void EnemyState(Enemy _enemy);
        public static EnemyState EnemyDeath;
        public static EnemyState EnemyConfusion;
        #endregion

        #region Animation
        Animator animator;

        private AnimationState _animState;
        public AnimationState AnimState
        {
            get { return _animState; }
            set
            {
                if (_animState == value)
                    return;

                _animState = value;
                if (animator != null)
                {
                    switch (_animState)
                    {
                        case AnimationState.Idle:
                            animator.CrossFade("Idle", 0.3f);
                            break;
                        case AnimationState.Walk:
                            animator.CrossFade("Walk", 0.3f);
                            break;
                        case AnimationState.Run:
                            animator.CrossFade("Run", 0.3f);
                            break;
                        case AnimationState.MeleeAttack:
                            animator.CrossFade("MeleeAttack", 0.3f);
                            break;
                        case AnimationState.RangedAttack:
                            animator.CrossFade("RangedAttack", 0.3f);
                            break;
                        case AnimationState.Damage:
                            animator.CrossFade("Damage", 0.3f);
                            break;
                        case AnimationState.Death:
                            animator.CrossFade("Death", 0.3f);
                            break;
                    }
                }
            }
        }

        public enum AnimationState
        {
            Idle = 0,
            Walk,
            Run,
            MeleeAttack,
            RangedAttack,
            Damage,
            Death,
        }
        #endregion
    }

    public enum EnemyType
    {
        Melee = 0,
        Ranged,
        Fire,
        Water,
        Poison, 
        Thunder
    }

    public enum ElementalType
    {
        None = 0,
        Fire = 1,
        Water = 2,
        Poison = 3,
        Thunder = 4
    }
}

