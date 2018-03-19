using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class WeaponController : MonoBehaviour
    {
        Weapon[] weapons;

        public Weapon CurrentWeapon { get; set; }

        public void Init(List<BulletData> _bulletDatas)
        {
            weapons = GetComponents<Weapon>();
            CurrentWeapon = weapons[0];
            foreach (Weapon _weapon in weapons)
            {
                _weapon.Init(_bulletDatas);
            }
        }

        public void Shot(ElementalAmmo _selectedAmmo)
        {
            CurrentWeapon.SingleShot(_selectedAmmo);
        }

        public void SetCurrentWeapon(WeaponType _type)
        {
            CurrentWeapon = weapons[(int)_type];
        }
    }
}