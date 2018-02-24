﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TeamF
{
    public class Enemy : MonoBehaviour, IDamageable, IParalyzable
    {
        public float Life { get { return data.Life; } set { data.Life = value; } }
        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }
        public float MovementSpeed { get { return agent.speed; } set { agent.speed = value; } }
        public EnemyData data { get; set; }
        public IEnemyBehaviour CurrentBehaviour { get; set; }
        public string SpecificID { get; set; }
        public IDamageable target { get; set; }

        NavMeshAgent agent;
        EnemyController controller;
        float attackTimeCounter;
        float agentTimeCounter;

        public void Init(Character _target, EnemyController _controller, string _id, EnemyData _data)
        {
            target = _target;
            controller = _controller;
            SpecificID = _id;

            data = _data;
            DeterminateBehaviourFromType(data);

            Instantiate(data.ModelPrefab, transform.position, transform.rotation, transform);           // Instanza il modello

            agent = GetComponentInChildren<NavMeshAgent>();
            agent.stoppingDistance = data.DamageRange;
            agent.SetDestination(_target.transform.position);

            CurrentBehaviour.DoInit(this);
        }

        public void SetPercentageOfMovementSpeed(float _movement)
        {
            MovementSpeed += (MovementSpeed * _movement) / 100;
        }

        /// <summary>
        /// Setta il nuovo target con l'idamageable più vicino a se
        /// </summary>
        public void ChangeMyTarget()
        {
            target = controller.GetCloserTarget(this);
        }

        private void FixedUpdate()
        {
            if (target == null)
                return;

            if (target.Life > 0)
            {
                Move();
                Attack(); 
            }
            else
                ChangeMyTarget();

            CheckMovementConstrains();
        }

        void Move()
        {
            agentTimeCounter += Time.deltaTime;
            if (agentTimeCounter >= 0.3f)
            {
                agent.SetDestination(target.Position);
                agentTimeCounter = 0;
            }
        }

        void RotateTowards(Vector3 _pointToLook)
        {
            transform.rotation = Quaternion.LookRotation(_pointToLook - transform.position, Vector3.up);
        }

        #region IDamageable
        float _damageMultiplyer = 100;
        public float DamageMultiplier {
            get { return _damageMultiplyer; }
            set { _damageMultiplyer = value; }
        }

        

        /// <summary>
        /// Funzione per prendere danno;
        /// </summary>
        /// <param name="_damage">Il valore da sottrarre alla vita</param>
        /// <param name="_bulletType">Il tipo del proiettile</param>
        public void TakeDamage(float _damage, ElementalType _bulletType)
        {
            _damage += (_damage * DamageMultiplier) / 100;
            CurrentBehaviour.DoTakeDamage(this, _damage, _bulletType);

            if (data.Life <= 0)
            {
                controller.KillEnemy(this);
                CurrentBehaviour.DoDeath(_bulletType);
                Destroy(gameObject);
            }
        }
        #endregion

        #region IParalizer
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

        void CheckMovementConstrains()
        {
            if (transform.rotation.x != 0 || transform.rotation.z != 0)
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        /// <summary>
        /// provoca danno alla vita del target se è alla distanza corretta
        /// </summary>
        void Attack()
        {
            attackTimeCounter += Time.deltaTime;
            if (attackTimeCounter >= data.DamageRate)
            {
                if (Vector3.Distance(agent.destination, transform.position) <= agent.stoppingDistance)
                {
                    if (target.Life > 0)
                    {
                        RotateTowards(target.Position);
                        CurrentBehaviour.DoAttack();
                        attackTimeCounter = 0;
                    }
                }
            }
        }

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