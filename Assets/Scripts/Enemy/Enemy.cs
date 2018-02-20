using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TeamF
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public float Life;
        public float MovementSpeed { get { return navMesh.speed; } set { navMesh.speed = value; } }
        public int Damage;
        public float DamageRange;
        public float DamageRate;

        public float EnemyValue;

        public string SpecificID { get; set; }
        NavMeshAgent navMesh;
        EnemyController controller;
        public Character target { get; set; }
        float time;

        public IEnemyBehaviour currentBehaviour;

        public void Init(Character _target, EnemyController _controller, string _id, IEnemyBehaviour _behaviour)
        {
            target = _target;
            controller = _controller;
            SpecificID = _id;

            currentBehaviour = _behaviour;
            currentBehaviour.DoInit(this);

            navMesh = GetComponent<NavMeshAgent>();
            navMesh.stoppingDistance = DamageRange;
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
            currentBehaviour.DoTakeDamage(this, _damage, _bulletType);

            if (Life <= 0)
            {
                controller.KillEnemy(this);
                currentBehaviour.DoDeath();
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
            if (time >= DamageRate)
            {
                if (DamageRange >= Vector3.Distance(transform.position, target.transform.position))
                {
                    currentBehaviour.DoAttack();
                    time = 0;
                }
            }
        }
    }
}