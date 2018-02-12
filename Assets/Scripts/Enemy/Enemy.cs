using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TeamF
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        ElementalType enemyType;
        public int Life;
        public float MovementSpeed;
        public int Damage;
        public float DamageRange;
        public float DamageRate;

        public float EnemyValue;

        public string SpecificID { get; set; }
        NavMeshAgent navMesh;
        EnemySpawner spawner;
        AvatarController target;
        float time;

        public void Init(AvatarController _target, EnemySpawner _spawner, string _id, ElementalType _type = ElementalType.None)
        {
            target = _target;
            spawner = _spawner;
            SpecificID = _id;
            enemyType = _type;
            navMesh = GetComponent<NavMeshAgent>();
            navMesh.stoppingDistance = DamageRange;
        }

        private void FixedUpdate()
        {
            if (target == null)
                return;

            navMesh.destination = target.transform.position;

            CheckmovementConstrains();
            //Move();
            //Rotate();
            Attack();
        }

        /// <summary>
        /// Funzione per prendere danno;
        /// </summary>
        /// <param name="_damage">Il valore da sottrarre alla vita</param>
        /// <param name="_bulletType">Il tipo del proiettile</param>
        public void TakeDamage(int _damage, ElementalType _bulletType)
        {
            if (enemyType == _bulletType)
            {
                print("Immune");
            }
            else
            {
                switch (_bulletType)
                {
                    case ElementalType.Fire:
                        print("In Fiamme");
                        break;
                    case ElementalType.Water:
                        print("Bagnato");
                        break;
                    case ElementalType.Poison:
                        print("Avvelenato");
                        break;
                    case ElementalType.Thunder:
                        print("Elettrificato");
                        break;
                }
                Life -= _damage;
                if (Life <= 0)
                {
                    spawner.KillEnemy(this);
                    Destroy(gameObject);
                }
            }
        }

        void CheckmovementConstrains()
        {
            if (transform.rotation.x != 0 || transform.rotation.z != 0)
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        /// <summary>
        /// [deprecata]
        /// </summary>
        void Move()
        {
            if (DamageRange <= Vector3.Distance(transform.position, target.transform.position))
                transform.position = Vector3.Lerp(transform.position, target.transform.position, (MovementSpeed * Time.deltaTime) / Vector3.Distance(transform.position, target.transform.position));
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
                    target.TakeDamage(Damage, enemyType);
                    time = 0;
                }
            }

        }

        /// <summary>
        /// [deprecata]
        /// </summary>
        void Rotate()
        {
            Quaternion.LookRotation(transform.position - target.transform.position);
        }
    }
}