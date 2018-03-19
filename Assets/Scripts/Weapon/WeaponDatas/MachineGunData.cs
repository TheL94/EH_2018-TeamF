using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    [CreateAssetMenu(fileName = "MachineGunData", menuName = "Weapon/MachineGunData")]
    public class MachineGunData : WeaponData
    {
        public float MaxOverheating;
        public float OverheatingPerShot;
        public float OverheatingDropSpeed;
    }
}