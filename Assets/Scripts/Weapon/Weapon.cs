using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Weapon : MonoBehaviour
    {
        public GameObject Barrel;
        GameObject BulletPrefab;
        float bulletSpeed;
        float ratio;
        int magCapacity;

        float ratioTimer;

        public void Init(float _bulletSpeed, float _ratio, int _magCapacity, GameObject _bulletPrefab)
        {
            bulletSpeed = _bulletSpeed;
            ratio = _ratio;
            magCapacity = _magCapacity;
            BulletPrefab = _bulletPrefab;
        }

        public ElementalAmmo FullAutoShoot(ElementalAmmo _selectedAmmo)
        {
            ratioTimer += Time.deltaTime;
            if (ratioTimer >= ratio)
            {
                _selectedAmmo = SingleShot(_selectedAmmo);
                ratioTimer = 0;
            }
            return _selectedAmmo;
        }

        /// <summary>
        /// Controlla se ci sono abbastanza munizioni o sono sotto zero (le munizioni standard) per chiamare la funzione per istanziare il proiettile.
        /// Se le munizioni sono maggiori di zero le scala.
        /// </summary>
        public ElementalAmmo SingleShot(ElementalAmmo _selectedAmmo)
        {
            if (_selectedAmmo.Ammo != 0)
            {
                CreateBullet(_selectedAmmo);
                if(_selectedAmmo.Ammo > 0)
                {
                    _selectedAmmo.Ammo--;
                }
            }
            return _selectedAmmo;
        }

        /// <summary>
        /// Istanzia il proiettile e ne chiama l'init
        /// </summary>
        void CreateBullet(ElementalAmmo _currentAmmo)
        {
            Bullet bull = Instantiate(BulletPrefab, Barrel.transform.position, Barrel.transform.rotation).GetComponent<Bullet>();
            bull.Init(_currentAmmo, bulletSpeed, BulletOwner.Character);
        }
    }
}