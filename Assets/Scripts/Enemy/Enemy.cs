using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TeamF
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public float MovementSpeed { get { return navMesh.speed; } set { navMesh.speed = value; } }

        public EnemyData data { get; set; }

        public string SpecificID { get; set; }
        NavMeshAgent navMesh;
        EnemyController controller;
        public Character target { get; set; }
        float time;

        public IEnemyBehaviour CurrentBehaviour { get; set; }

        public void Init(Character _target, EnemyController _controller, string _id, EnemyData _data)
        {
            target = _target;
            controller = _controller;
            SpecificID = _id;

            data = _data;
            DeterminateBehaviourFromType();

            Instantiate(data.ModelPrefab, transform);           // Instanza il modello

            CurrentBehaviour.DoInit(this);

            navMesh = GetComponentInChildren<NavMeshAgent>();
            navMesh.stoppingDistance = data.DamageRange;
        }

        private void FixedUpdate()
        {
            if (target == null)
                return;

            navMesh.destination = target.transform.position;

            CheckMovementConstrains();
            Attack();
        }

        /// <summary>
        /// Funzione per prendere danno;
        /// </summary>
        /// <param name="_damage">Il valore da sottrarre alla vita</param>
        /// <param name="_bulletType">Il tipo del proiettile</param>
        public void TakeDamage(float _damage, ElementalType _bulletType)
        {
            CurrentBehaviour.DoTakeDamage(this, _damage, _bulletType);

            if (data.Life <= 0)
            {
                controller.KillEnemy(this);
                CurrentBehaviour.DoDeath();
                Destroy(gameObject);
            }
        }

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
            time += Time.deltaTime;
            if (time >= data.DamageRate)
            {
                if (data.DamageRange >= Vector3.Distance(transform.position, target.transform.position))
                {
                    CurrentBehaviour.DoAttack();
                    time = 0;
                }
            }
        }

        void DeterminateBehaviourFromType()
        {
            switch (data.EnemyType)
            {
                case ElementalType.None:
                    CurrentBehaviour = new EnemyBehaviourBase();
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
                default:
                    CurrentBehaviour = new EnemyBehaviourBase();
                    break;
            }
        }
    }
}