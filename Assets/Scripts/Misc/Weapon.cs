using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Weapon : MonoBehaviour
    {

        public GameObject BulletPrefab;
        public GameObject Barrel;
        public int MagCapacity;
        public float BulletSpeed;
        public float Ratio;
        public int Damage;

        float ratioTimer;

        private int _ammo = 10;

        public int Ammo
        {
            get { return _ammo; }
            set
            {
                _ammo = value;
                GameManager.I.UIMng.UI_GameplayCtrl.UpdateAmmo(_ammo);
            }
        }

        public void AddAmmo(int _ammoToAdd)
        {
            if (Ammo < MagCapacity)
            {
                Ammo += _ammoToAdd;
                if (Ammo > MagCapacity)
                    Ammo = MagCapacity;
            }
        }

        public void FullAutoShoot()
        {
            ratioTimer += Time.deltaTime;
            if (ratioTimer >= Ratio)
                SingleShot();
        }

        public void SingleShot()
        {
            if (Ammo > 0)
            {
                Bullet bull = Instantiate(BulletPrefab, Barrel.transform.position, Barrel.transform.rotation).GetComponent<Bullet>();
                bull.Init(Damage, BulletSpeed);
                ratioTimer = 0;
                Ammo--;
            }
        }

    }
}