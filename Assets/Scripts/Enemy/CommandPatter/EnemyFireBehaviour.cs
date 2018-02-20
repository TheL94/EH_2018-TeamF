﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyFireBehaviour : EnemyBehaviourBase
    {
        public override void DoInit(Enemy _myEnemy)
        {
            base.DoInit(_myEnemy);
            _myEnemy.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        }

        public override void DoTakeDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            if (_type == ElementalType.Water)
                _damage *= 1.5f;
            if (_type == ElementalType.Fire)
                _damage = 0;
            base.DoTakeDamage(_enemy, _damage, _type);
        }
    }
}