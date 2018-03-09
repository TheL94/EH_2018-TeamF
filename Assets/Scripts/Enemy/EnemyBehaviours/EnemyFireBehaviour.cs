using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyFireBehaviour : EnemyBehaviourMelee
    {
        public override void DoInit(Enemy _myEnemy)
        {
            base.DoInit(_myEnemy);
            _myEnemy.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        }

        public override float CalulateDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            if (_type == ElementalType.Water)
                _damage *= 1.5f;
            if (_type == ElementalType.Fire)
                _damage = 0;
            return base.CalulateDamage(_enemy, _damage, _type);
        }

        public override void DoDeath(ElementalType _bulletType)
        {
            if (_bulletType == ElementalType.Thunder)
                InstantiateElementalCombo("ElementalCombo/SlowingCloud");

            if (_bulletType == ElementalType.Water)
                InstantiateElementalCombo("ElementalCombo/BlackHole");

        }
    }
}