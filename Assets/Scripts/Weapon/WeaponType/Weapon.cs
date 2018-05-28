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
        Character character;

        #region API
        public void Init(Character _character)
        {
            character = _character;
            weaponData = Instantiate(WeaponData);
        }

        /// <summary>
        /// Controlla se ci sono abbastanza munizioni o sono sotto zero (le munizioni standard) per chiamare la funzione per istanziare il proiettile.
        /// Se le munizioni sono maggiori di zero le scala.
        /// </summary>
        public virtual void SingleShot(BulletData _bulletData, Transform _barrel)
        {
            if (Time.time >= nextFire)
            {
                if (_bulletData.ElementalAmmo.Ammo != 0)
                {
                    GameManager.I.AudioMng.PlaySound(Clips.CharacterShoot);
                    CreateBullet(_bulletData, _barrel);
                    if (_bulletData.ElementalAmmo.Ammo > 0)
                    {
                        _bulletData.ElementalAmmo.Ammo--;

                        if (_bulletData.ElementalAmmo.AmmoType != ElementalType.None)
                            Events_UIController.AmmoChange(_bulletData.ElementalAmmo);      // UI event
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
        void CreateBullet(BulletData _bulletData, Transform _barrel)
        {
            GameObject bullPrefab = Instantiate(_bulletData.BulletContainerPrefab, _barrel.position, _barrel.rotation);

            Bullet bull = bullPrefab.GetComponent<Bullet>();

            if (_bulletData.BulletGraphicPrefab != null)
                Instantiate(_bulletData.BulletGraphicPrefab, bull.transform.position, bull.transform.rotation, bull.transform);
            if (_bulletData.BulletTrailPrefab != null)
                Instantiate(_bulletData.BulletTrailPrefab, bull.transform.position, bull.transform.rotation, bull.transform);

            bull.Init(_bulletData.ElementalAmmo, weaponData.Parameters.BulletSpeed, character, weaponData.Parameters.BulletRange, weaponData.Parameters.DamagePercentage);
        }
    }
}