using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ManichinoDestinazione : MonoBehaviour, IDamageable
    {
        public float Life { get; set; }

        public Vector3 Position { get { return transform.position; } set { transform.position = value; } }

        public float DamagePercentage { get; set; }

        public void TakeDamage(float _damage, ElementalType _type = ElementalType.None)
        {
            
        }
    }
}