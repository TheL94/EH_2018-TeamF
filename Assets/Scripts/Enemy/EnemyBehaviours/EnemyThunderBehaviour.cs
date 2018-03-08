using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyThunderBehaviour : EnemyBehaviourMelee
    {
        public override void DoInit(Enemy _myEnemy)
        {
            base.DoInit(_myEnemy);
            _myEnemy.GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
        }

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
                GameObject.Instantiate(Resources.Load("ElementalCombo/SlowingCloud"), myEnemy.transform.position, Quaternion.identity);
            if (_bulletType == ElementalType.Poison)
                GameObject.Instantiate(Resources.Load("ElementalCombo/ConfusionCloud"), myEnemy.transform.position, Quaternion.identity); 

            base.DoDeath(_bulletType);
        }
    }
}