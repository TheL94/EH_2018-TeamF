using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyPoisonBehaviour : EnemyBehaviourMelee
    {
        public override void DoInit(Enemy _myEnemy)
        {
            base.DoInit(_myEnemy);
            _myEnemy.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        }

        public override void DoTakeDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            if(_type == ElementalType.Fire)
                _damage *= 1.5f;
            if (_type == ElementalType.Poison)
                _damage = 0;
            base.DoTakeDamage(_enemy, _damage, _type);
        }
    }
}