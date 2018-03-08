using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public interface IDamageable
    {
        float Life { get; }
        Vector3 Position { get; }
        float DamagePercentage { get; set; }

        void TakeDamage(float _damage, ElementalType _type = ElementalType.None);
    }
}