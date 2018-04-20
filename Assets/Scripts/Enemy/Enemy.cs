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
    public class Enemy : MonoBehaviour, IEffectable, ICharmable
    {
        public EnemyGenericData Data { get; private set; }
        public string ID { get; private set; }

        #region IGetSlower
        public bool IsSlowed { get; set; }
        public float MovementSpeed
        {
            get { return Agent.speed; }
            set
            {
                Data.Speed = value;
                Agent.speed = Data.Speed;

            }
        }
        #endregion

        public NavMeshAgent Agent { get; private set; }
        public IDamageable Target { get; set; }
        public AI_Enemy AI_Enemy { get; private set; }

        MeshRenderer render;

        public void Init(EnemyGenericData _data, string _id)
        {
            Data = _data;
            ID = _id;
            Life = Data.Life;

            //Instantiate(GameManager.I.PoolMng.GetObject(Data.GraphicID), transform.position, transform.rotation, transform);
            GetGraphic();

            Agent = GetComponent<NavMeshAgent>();
            AI_Enemy = GetComponent<AI_Enemy>();
            render = GetComponentInChildren<MeshRenderer>();
            Animator = GetComponentInChildren<Animator>();

            CurrentBehaviour = DeterminateBehaviourFromType(Data);

            AI_Enemy.InitialDefaultState = Data.InitialState;
            AI_Enemy.IsActive = true;
        }

        void GetGraphic()
        {
            GameObject graphic = GameManager.I.PoolMng.GetObject(Data.GraphicID);
            graphic.transform.position = transform.position;
            graphic.transform.SetParent(transform);
        }

        #region IEnemyBehaviour
        public IEnemyBehaviour CurrentBehaviour { get; private set; }

        IEnemyBehaviour DeterminateBehaviourFromType(EnemyGenericData _data)
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
            LastHittingBulletType = _type;
            _damage = (_damage * DamagePercentage) / 100;
            Life -= CurrentBehaviour.CalulateDamage(_damage, _type);

            AI_Enemy.CurrentState = Data.DamageState;

            if (render != null)              
                render.material.DOColor(Color.white, .1f).OnComplete(() => { render.material.DORewind(); });
        }

        public ElementalType LastHittingBulletType { get; private set; }
        #endregion

        #region IParalyzable
        /// <summary>
        /// Chiamata dalla combo elementale paralizzante
        /// </summary>

        bool _isParalized;
        public bool IsParalized
        {
            get { return _isParalized; }
            set
            {
                _isParalized = value;
                if (_isParalized)
                    AI_Enemy.CurrentState = Data.ParalizedState;
            }
        }
        #endregion

        #region ICharmable
        bool _isCharmed;
        /// <summary>
        /// Chiamata dalla combo elementale paralizzante
        /// </summary>
        public bool IsCharmed
        {
            get { return _isCharmed; }
            set
            {
                if(_isCharmed != value)
                {
                    _isCharmed = value;
                    AI_Enemy.SetAICurrentState(Data.CharmedState);
                }
            }
        }
        #endregion

        #region Animation

        public Animator Animator { get; private set; }

        #endregion

        #region Enemy Delegate
        public delegate void EnemyState(Enemy _enemy);
        public static EnemyState EnemyDeath;
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

    public enum AttackType
    {
        Melee = 0,
        Ranged
    }

    public enum ElementalType
    {
        None = 0,
        Fire,
        Water,
        Poison,
        Thunder
    }
}

