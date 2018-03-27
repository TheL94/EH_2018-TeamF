using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TeamF
{
    public class EnemyBehaviourRanged : IEnemyBehaviour
    {
        public float CalulateDamage(float _damage, ElementalType _damageType)
        {
            return _damage;
        }

        public void DoDeath(ElementalType _bulletType, Vector3 _position) { }
    }
}