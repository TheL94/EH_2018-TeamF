using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public interface IDamageable
    {

        void TakeDamage(float _damage, ElementalType _type = ElementalType.None);

    }
}