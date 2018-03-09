using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyWaterBehaviour : EnemyBehaviourMelee
    {
        public override void DoInit(Enemy _myEnemy)
        {
            base.DoInit(_myEnemy);
            _myEnemy.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        }

        public override float CalulateDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            if (_type == ElementalType.Thunder)
                _damage *= 1.5f;
            if (_type == ElementalType.Water)
                _damage = 0;
            return base.CalulateDamage(_enemy, _damage, _type);
        }

        public override void DoDeath(ElementalType _bulletType)
        {
            if (_bulletType == ElementalType.Poison)
                InstantiateElementalCombo("ElementalCombo/IncreaseDamage");

            if (_bulletType == ElementalType.Thunder)
                InstantiateElementalCombo("ElementalCombo/ParalyzingCloud");

        }
    }
}