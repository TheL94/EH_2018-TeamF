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

        float nextFire;

        #region API
        public void Init()
        {
            weaponData = Instantiate(WeaponData);
        }

        /// <summary>
        /// Controlla se ci sono abbastanza munizioni o sono sotto zero (le munizioni standard) per chiamare la funzione per istanziare il proiettile.
        /// Se le munizioni sono maggiori di zero le scala.
        /// </summary>
        public virtual void SingleShot(ElementalAmmo _selectedAmmo, BulletData _bulletData, Transform _barrel)
        {
            if (Time.time >= nextFire)
            {
                if (_selectedAmmo.Ammo != 0)
                {
                    CreateBullet(_selectedAmmo, _bulletData, _barrel);
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
        void CreateBullet(ElementalAmmo _currentAmmo, BulletData _bulletData, Transform _barrel)
        {
            Bullet bull = Instantiate(weaponData.BulletContainerPrefab, _barrel.position, _barrel.rotation).GetComponent<Bullet>();

            if(_bulletData.BulletGraphicPrefab != null)
                Instantiate(_bulletData.BulletGraphicPrefab, bull.transform.position, bull.transform.rotation, bull.transform);
            if (_bulletData.BulletTrailPrefab != null)
                Instantiate(_bulletData.BulletTrailPrefab, bull.transform.position, bull.transform.rotation, bull.transform);

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