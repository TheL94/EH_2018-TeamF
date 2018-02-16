using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Weapon : MonoBehaviour
    {
        Character character;
        public GameObject BulletPrefab;
        public GameObject Barrel;
        public int MagCapacity;
        public float BulletSpeed;
        public float Ratio;

        float ratioTimer;

        public void Init(Character _character)
        {
            character = _character;
        }

        public ElementalAmmo FullAutoShoot(ElementalAmmo _selectedAmmo)
        {
            ratioTimer += Time.deltaTime;
            if (ratioTimer >= Ratio)
                _selectedAmmo = SingleShot(_selectedAmmo);
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
            bull.Init(_currentAmmo, BulletSpeed);
            ratioTimer = 0;

        }
    }
}