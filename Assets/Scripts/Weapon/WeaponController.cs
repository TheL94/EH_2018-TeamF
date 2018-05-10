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
        }

        public void Shot(BulletData _selectedAmmo)
        {
            //BulletData data = bulletDatas.Where(d => d.ElementalAmmo == _selectedAmmo).First();
            CurrentWeapon.SingleShot(_selectedAmmo, Barrel.transform);
        }

        public void SetCurrentWeapon(WeaponType _type)
        {
            CurrentWeapon = weapons[(int)_type];
        }
    }
}