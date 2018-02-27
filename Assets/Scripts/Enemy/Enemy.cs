using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Framework.AI;

namespace TeamF
{
    [RequireComponent(typeof(NavMeshAgent), typeof(AI_Controller))]
    public class Enemy : MonoBehaviour, IDamageable, IParalyzable
    {
        public EnemyData Data { get; private set; }
        public string ID { get; private set; }

        EnemyController controller;

        #region API
        public void Init(IDamageable _target, EnemyController _controller, EnemyData _data, string _id)
        {
            agent = GetComponent<NavMeshAgent>();
            ai_Controller = GetComponent<AI_Controller>();

            Target = _target;
            controller = _controller;
            Data = _data;
            ID = _id;

            DeterminateBehaviourFromType(Data);

            Instantiate(Data.ModelPrefab, transform.position, transform.rotation, transform);

            agent.stoppingDistance = Data.DamageRange;
            agent.SetDestination(Target.Position);

            CurrentBehaviour.DoInit(this);
        }
        #endregion

        #region Nav Mesh Agent
        NavMeshAgent agent;

        IDamageable _target;
        public IDamageable Target
        {
            get { return _target; }
            set {
                if (value == null) // Se target è nullo chiede come target al controller il più vicino
                    _target = controller.GetClosestTarget(this);
                else
                    _target = value;
            }
        }
        #endregion

        #region AI Controller
        AI_Controller ai_Controller;
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

        #region Effects
        public float MovementSpeed
        {
            get { return agent.speed; }
            set { agent.speed += (agent.speed * value) / 100; }
        }
        #endregion

        #region IDamageable
        public float Life { get { return Data.Life; } private set { Data.Life = value; } }

        public Vector3 Position { get { return transform.position; } }

        float _damageMultiplyer = 100;
        public float DamagePercentage
        {
            get { return _damageMultiplyer; }
            set { _damageMultiplyer = value; }
        }

        public void TakeDamage(float _damage, ElementalType _type = ElementalType.None)
        {
            _damage += (_damage * DamagePercentage) / 100;
            CurrentBehaviour.DoTakeDamage(this, _damage, _type);

            if (Data.Life <= 0)
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
            if (agent.isActiveAndEnabled)
            {
                agent.isStopped = _isParalized;
            }
        }

        #endregion
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

