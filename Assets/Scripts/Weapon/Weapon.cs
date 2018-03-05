using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace TeamF
{
    public class Weapon : MonoBehaviour
    {
        public GameObject Barrel;
        float bulletSpeed;
        float ratio;
        int magCapacity;
        List<BulletData> bulletDatas;

        float ratioTimer;

        public void Init(float _bulletSpeed, float _ratio, int _magCapacity, List<BulletData> _bulletDatas)
        {
            bulletSpeed = _bulletSpeed;
            ratio = _ratio;
            magCapacity = _magCapacity;
            bulletDatas = _bulletDatas;
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
            BulletData data = bulletDatas.Where(d => d.Type == _currentAmmo.AmmoType).First();
            Bullet bull = Instantiate(data.BulletContainerPrefab, Barrel.transform.position, Barrel.transform.rotation).GetComponent<Bullet>();
            Instantiate(data.BulletGraphicPrefab, bull.transform.position, bull.transform.rotation, bull.transform);
            Instantiate(data.BulletTrailPrefab, bull.transform.position, bull.transform.rotation, bull.transform);
            bull.Init(_currentAmmo, bulletSpeed, BulletOwner.Character);
        }
    }
}