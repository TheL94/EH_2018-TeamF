using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyPoisonBehaviour : IEnemyBehaviour
    {
        public float CalulateDamage(float _damage, ElementalType _damageType)
        {
            if (_damageType == ElementalType.Fire)
                _damage *= 1.5f;
            if (_damageType == ElementalType.Poison)
                _damage = 0;
            return _damage;
        }

        public void DoDeath(ElementalType _bulletType, Vector3 _position)
        {
            if (_bulletType == ElementalType.Fire)
                this.InstantiateElementalCombo("ElementalCombo/FireExplosion", _position);
            if (_bulletType == ElementalType.Water)
                this.InstantiateElementalCombo("ElementalCombo/IncreaseDamage", _position);
        }
    }
}