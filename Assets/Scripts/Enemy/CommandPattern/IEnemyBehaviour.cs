using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public interface IEnemyBehaviour
    {
        void DoInit(Enemy _myEnemy);
        void DoTakeDamage(Enemy _enemy, float _damage, ElementalType _type);
        void DoAttack();
        void DoDeath(ElementalType _bulletType);
    }
}