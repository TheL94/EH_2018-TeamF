using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public interface IDamageable
    {
        float Life { get; set; }
        Vector3 Position { get; set; }
        float DamageMultiplier { get; set; }
        void TakeDamage(float _damage, ElementalType _type = ElementalType.None);
    }
}