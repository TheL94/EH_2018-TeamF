using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityFramework.AI;
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
                Data.Speed = value;
                Agent.speed = Data.Speed;

            }
        }

        MeshRenderer render;
        AI_Enemy ai_Enemy;

        #region Navigation
        public NavMeshAgent Agent { get; private set; }
        public IDamageable Target { get; set; }
        #endregion

        public void Init(IDamageable _target, EnemyData _data, AI_State _initalState, string _id)
        {
            Target = _target;
            Data = _data;
            ID = _id;
            Life = Data.Life;
            
            Instantiate(Data.GraphicPrefab, transform.position, transform.rotation, transform);

            Agent = GetComponent<NavMeshAgent>();
            ai_Enemy = GetComponent<AI_Enemy>();
            render = GetComponentInChildren<MeshRenderer>();
            Animator = GetComponentInChildren<Animator>();

            CurrentBehaviour = DeterminateBehaviourFromType(Data);

            Agent.speed = Data.Speed;
            Agent.stoppingDistance = Data.MeleeDamageRange;
            Agent.SetDestination(Target.Position);

            ai_Enemy.InitialDefaultState = _initalState;
            ai_Enemy.IsActive = true;
        }

        #region Actions
        void DeathActions(ElementalType _type)
        {
            if (Animator != null)
                AnimState = AnimationState.Death;

            // TODO : risolto bug in modo scoretto --------------------
            EffectController effect = GetComponent<EffectController>();
            if (effect != null)
                effect.gameObject.SetActive(false);
            // --------------------------------------------------------

            CurrentBehaviour.DoDeath(_type, transform.position);

            ai_Enemy.IsActive = false;
            if (EnemyDeath != null)
                EnemyDeath(this);
        }
        #endregion

        #region IEnemyBehaviour
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
            Life -= CurrentBehaviour.CalulateDamage(_damage, _type);

            if (Animator != null)
                AnimState = AnimationState.Damage;
            else
                render.material.DOColor(Color.white, .1f).OnComplete(() => { render.material.DORewind(); });

            if (Life <= 0)
            {
                DeathActions(_type);
            }
        }
        #endregion

        #region IParalyzable
        /// <summary>
        /// Chiamata dalla combo elementale paralizzante
        /// </summary>
        public bool IsParalized { get; set; }
        #endregion

        #region Animation
        public Animator Animator { get; private set; }

        private AnimationState _animState;
        public AnimationState AnimState
        {
            get { return _animState; }
            set
            {
                if (_animState == value)
                    return;

                _animState = value;
                if (Animator != null)
                {
                    switch (_animState)
                    {
                        case AnimationState.Idle:
                            Animator.CrossFade("Idle", 0.3f);
                            break;
                        case AnimationState.Walk:
                            Animator.CrossFade("walk", 0.3f);
                            break;
                        case AnimationState.Run:
                            Animator.CrossFade("run", 0.3f);
                            break;
                        case AnimationState.MeleeAttack:
                            Animator.CrossFade("melee", 0.3f);
                            break;
                        case AnimationState.RangedAttack:
                            Animator.CrossFade("range", 0.3f);
                            break;
                        case AnimationState.Damage:
                            Animator.CrossFade("damage", 0.3f);
                            break;
                        case AnimationState.Death:
                            Animator.CrossFade("death", 0.3f);
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

        #region Enemy Delegate
        public delegate void EnemyState(Enemy _enemy);
        public static EnemyState EnemyDeath;
        public static EnemyState EnemyConfusion;
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

