using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public interface IBulletEffectBehaviour
    {
        void DoInit(Enemy _enemy, float _value);
        void DoEffect();
        void DoStopEffect();
    }
}