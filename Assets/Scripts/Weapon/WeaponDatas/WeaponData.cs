using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public WeaponType Weapon;
        public WeaponParameters Parameters;
    }

    [System.Serializable]
    public struct WeaponParameters
    {
        public float BulletSpeed;
        public float BulletRange;
        public float Ratio;
        public float DamagePercentage;
    }

    public enum WeaponType
    {
        Pistol,
        MachineGun
    }
}