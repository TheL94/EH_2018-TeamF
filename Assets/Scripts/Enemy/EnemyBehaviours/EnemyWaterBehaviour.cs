using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyWaterBehaviour : IEnemyBehaviour
    {
        public float CalulateDamage(float _damage, ElementalType _damageType)
        {
            if (_damageType == ElementalType.Thunder)
                _damage *= 1.5f;
            if (_damageType == ElementalType.Water)
                _damage = 0;
            return _damage;
        }

        public void DoDeath(ElementalType _bulletType, Vector3 _position)
        {
            if (_bulletType == ElementalType.Poison)
                this.InstantiateElementalCombo("ElementalCombo/IncreaseDamage", _position);

            if (_bulletType == ElementalType.Thunder)
                this.InstantiateElementalCombo("ElementalCombo/ParalyzingCloud", _position);
        }
    }
}