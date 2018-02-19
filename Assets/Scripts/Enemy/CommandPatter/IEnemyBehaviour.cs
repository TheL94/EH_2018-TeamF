using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public interface IEnemyBehaviour
    {
        void DoInit();
        void TakeDamage(Enemy _enemy, float _damage, ElementalType _type);
        void DoAttack();
        void DoDeath();

    }
}