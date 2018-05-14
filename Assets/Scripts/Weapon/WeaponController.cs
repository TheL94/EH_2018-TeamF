using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TeamF
{
    public class WeaponController : MonoBehaviour
    {
        Transform Barrel;
        Weapon[] weapons;
        List<BulletData> bulletDatas;

        public Weapon CurrentWeapon { get; set; }

        public void Init(List<BulletData> _bulletDatas, Character _character)
        {
            weapons = GetComponents<Weapon>();
            bulletDatas = _bulletDatas;
            Barrel = GetComponentsInChildren<Transform>().Where(d => d.tag == "Barrel").First();
            CurrentWeapon = weapons[0];

            foreach (Weapon item in weapons)
            {
                item.Init(_character);
            }

            foreach (BulletData bulletData in _bulletDatas)
            {
                if (bulletData.ElementalAmmo.AmmoType != ElementalType.None)
                    Events_UIController.AmmoChange(bulletData.ElementalAmmo);      // UI event
            }
        }

        public void Shot(BulletData _selectedAmmo)
        {
            CurrentWeapon.SingleShot(_selectedAmmo, Barrel.transform);
        }

        public void SetCurrentWeapon(WeaponType _type)
        {
            CurrentWeapon = weapons[(int)_type];
        }
    }
}