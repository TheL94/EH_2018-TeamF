﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemyThunderBehaviour : EnemyBehaviourBase
    {
        public override void DoTakeDamage(Enemy _enemy, float _damage, ElementalType _type)
        {
            if (_type == ElementalType.Poison)
                _damage *= 1.5f;
            if (_type == ElementalType.Thunder)
                _damage = 0;
            base.DoTakeDamage(_enemy, _damage, _type);
        }
    }
}