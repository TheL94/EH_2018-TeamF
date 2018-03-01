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

        // da tolgiere possibilmente
        EnemyController controller;

        MeshRenderer render;

        #region API
        public void Init(IDamageable _target, EnemyController _controller, EnemyData _data, string _id)
        {
            Target = _target;
            controller = _controller;
            Data = _data;
            ID = _id;

            Instantiate(Data.ModelPrefab, transform.position, transform.rotation, transform);

            Agent = GetComponent<NavMeshAgent>();
            ai_Enemy = GetComponent<AI_Enemy>();
            render = GetComponentInChildren<MeshRenderer>();

            DeterminateBehaviourFromType(Data);

            Agent.speed = Data.Speed;
            Agent.stoppingDistance = Data.DamageRange;              //TODO: incorretto non va fatto così.
            Agent.SetDestination(Target.Position);

            CurrentBehaviour.DoInit(this);

            ai_Enemy.IsActive = true;
        }
        #endregion

        #region Nav Mesh Agent
        public NavMeshAgent Agent { get; private set; }

        IDamageable _target;
        public IDamageable Target
        {
            get { return _target; }
            set
            {
                if (value == null) // Se target è nullo chiede come target al controller il più vicino
                    _target = controller.GetClosestTarget(this); // cambiare il modo in cui viene chiamata questa funzione (dall'alto verso il basso)
                else
                    _target = value;
            }
        }
        #endregion

        #region AI Controller
        AI_Enemy ai_Enemy;
        AI_State currentState;
        #endregion

        #region Enemy Behaviour
        public IEnemyBehaviour CurrentBehaviour { get; private set; }

        void DeterminateBehaviourFromType(EnemyData _data)
        {
            switch (_data.EnemyType)
            {
                case EnemyType.Melee:
                    switch (_data.ElementalType)
                    {
                        case ElementalType.None:
                            CurrentBehaviour = new EnemyBehaviourMelee();
                            break;
                        case ElementalType.Fire:
                            CurrentBehaviour = new EnemyFireBehaviour();
                            break;
                        case ElementalType.Water:
                            CurrentBehaviour = new EnemyWaterBehaviour();
                            break;
                        case ElementalType.Poison:
                            CurrentBehaviour = new EnemyPoisonBehaviour();
                            break;
                        case ElementalType.Thunder:
                            CurrentBehaviour = new EnemyThunderBehaviour();
                            break;
                    }
                    break;
                case EnemyType.Ranged:
                    switch (_data.ElementalType)
                    {
                        case ElementalType.None:
                            CurrentBehaviour = new EnemyBehaviourRanged();
                            break;
                        case ElementalType.Fire:
                            CurrentBehaviour = new EnemyFireBehaviour();
                            break;
                        case ElementalType.Water:
                            CurrentBehaviour = new EnemyWaterBehaviour();
                            break;
                        case ElementalType.Poison:
                            CurrentBehaviour = new EnemyPoisonBehaviour();
                            break;
                        case ElementalType.Thunder:
                            CurrentBehaviour = new EnemyThunderBehaviour();
                            break;
                    }
                    break;
            }
        }
        #endregion

        #region IDamageable
        public float Life
        {
            get { return Data.Life; }
            private set
            {
                Data.Life = value;
                render.material.DOColor(Color.white, .1f).OnComplete(() => { render.material.DORewind(); });
            }
        }
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

            if (Life <= 0)
            {
                CurrentBehaviour.DoDeath(_type);
                // distrggere l'oggetto e avvisare il controller
            }
        }
        #endregion

        #region IParalyzable
        /// <summary>
        /// Chiamata dalla combo elementale paralizzante, 
        /// </summary>
        /// <param name="_isParalize"></param>
        public void Paralize(bool _isParalized)
        {
            if (Agent.isActiveAndEnabled)
            {
                Agent.isStopped = _isParalized;
            }
        }
        #endregion

        void CheckMovementConstrains()
        {
            if (transform.rotation.x != 0 || transform.rotation.z != 0)
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }

    public enum EnemyType
    {
        Melee = 0,
        Ranged
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

