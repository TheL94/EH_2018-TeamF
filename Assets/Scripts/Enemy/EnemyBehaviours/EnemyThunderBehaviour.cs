using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyThunderBehaviour : EnemyBehaviourMelee
    {
        public override float CalulateDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            if (_type == ElementalType.Poison)
                _damage *= 1.5f;
            if (_type == ElementalType.Thunder)
                _damage = 0;
            return base.CalulateDamage(_enemy, _damage, _type);
        }

        public override void DoDeath(ElementalType _bulletType)
        {
            if(_bulletType == ElementalType.Fire)
                InstantiateElementalCombo("ElementalCombo/SlowingCloud");

            if (_bulletType == ElementalType.Poison)
                InstantiateElementalCombo("ElementalCombo/IncreaseDamage");


            base.DoDeath(_bulletType);
        }
    }
}