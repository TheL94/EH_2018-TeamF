using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyBehaviourMelee : IEnemyBehaviour
    {
        public float CalulateDamage(float _damage, ElementalType _damageType)
        {
            return _damage;
        }

        public void DoDeath(ElementalType _bulletType, Vector3 _position) { }
    }
}