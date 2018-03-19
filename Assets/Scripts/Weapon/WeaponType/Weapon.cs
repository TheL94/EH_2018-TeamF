using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace TeamF
{
    public class Weapon : MonoBehaviour
    {
        public WeaponData WeaponData;

        protected WeaponData weaponData;
        List<BulletData> bulletDatas;
        GameObject Barrel;

        float nextFire;

        #region API
        public void Init(List<BulletData> _bulletDatas)
        {
            bulletDatas = _bulletDatas;
            weaponData = Instantiate(WeaponData);
            Barrel = GetComponentsInChildren<Transform>().Where(d => d.tag == "Barrel").First().gameObject;
        }

        public void SetWeaponData(WeaponData _data)
        {
            weaponData = _data;
        }

        /// <summary>
        /// Controlla se ci sono abbastanza munizioni o sono sotto zero (le munizioni standard) per chiamare la funzione per istanziare il proiettile.
        /// Se le munizioni sono maggiori di zero le scala.
        /// </summary>
        public virtual void SingleShot(ElementalAmmo _selectedAmmo)
        {
            if (Time.time >= nextFire)
            {
                if (_selectedAmmo.Ammo != 0)
                {
                    CreateBullet(_selectedAmmo);
                    if (_selectedAmmo.Ammo > 0)
                    {
                        _selectedAmmo.Ammo--;

                        if (_selectedAmmo.AmmoType != ElementalType.None)
                            Events_UIController.AmmoChange(_selectedAmmo);      // UI event
                    }
                    OnShot();
                    nextFire = Time.time + weaponData.Parameters.Ratio;
                }
            }
        }

        /// <summary>
        /// Chiamata quando viene effettuato uno sparo
        /// </summary>
        protected virtual void OnShot() { }

        #endregion
        /// <summary>
        /// Istanzia il proiettile e ne chiama l'init
        /// </summary>
        void CreateBullet(ElementalAmmo _currentAmmo)
        {
            BulletData data = bulletDatas.Where(d => d.Type == _currentAmmo.AmmoType).First();
            Bullet bull = Instantiate(weaponData.BulletContainerPrefab, Barrel.transform.position, Barrel.transform.rotation).GetComponent<Bullet>();

            if(data.BulletGraphicPrefab != null)
                Instantiate(data.BulletGraphicPrefab, bull.transform.position, bull.transform.rotation, bull.transform);
            if (data.BulletTrailPrefab != null)
                Instantiate(data.BulletTrailPrefab, bull.transform.position, bull.transform.rotation, bull.transform);

            bull.Init(_currentAmmo, weaponData.Parameters.BulletSpeed, BulletOwner.Character, weaponData.Parameters.BulletLife,ChoseBulletBehaviour());
        }

        IBulletBehaviour ChoseBulletBehaviour()
        {
            switch (weaponData.Weapon)
            {
                case WeaponType.Pistol:
                    return new PistolBulletBehaviour();
                case WeaponType.MachineGun:
                    return new MachineGunBulletBehaviour();
                case WeaponType.ShotGun:
                    return new ShotgunBulletBehaviour();
                case WeaponType.Ballistra:
                    return new BallistraBulletBehaviour();
                case WeaponType.SniperRifle:
                    return new SniperRifleBulletBehaviour();
                default:
                    return new PistolBulletBehaviour();
            }
        }
    }
}