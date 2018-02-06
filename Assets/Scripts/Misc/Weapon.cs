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


        public void FullAutoShoot()
        {
            ratioTimer += Time.deltaTime;
            if (ratioTimer >= Ratio)
                SingleShot();
        }

        public void SingleShot()
        {
            Bullet bull = Instantiate(BulletPrefab, Barrel.transform.position, Barrel.transform.rotation).GetComponent<Bullet>();
            bull.Init(Damage, BulletSpeed);
            ratioTimer = 0;
        }

    }
}