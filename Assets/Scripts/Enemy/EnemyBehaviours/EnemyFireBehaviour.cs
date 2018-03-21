using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyFireBehaviour : IEnemyBehaviour
    {
        public float CalulateDamage(float _damage, ElementalType _damageType)
        {
            if (_damageType == ElementalType.Water)
                _damage *= 1.5f;
            if (_damageType == ElementalType.Fire)
                _damage = 0;
            return _damage;
        }

        public void DoDeath(ElementalType _bulletType, Vector3 _position)
        {
            if (_bulletType == ElementalType.Thunder)
                this.InstantiateElementalCombo("ElementalCombo/SlowingCloud", _position);
            if (_bulletType == ElementalType.Water)
                this.InstantiateElementalCombo("ElementalCombo/BlackHole", _position);
        }
    }
}