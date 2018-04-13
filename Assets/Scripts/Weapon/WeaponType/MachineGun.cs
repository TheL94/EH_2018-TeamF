using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class MachineGun : Weapon
    {
        private float _overheating;

        public float Overheating
        {
            get { return _overheating; }
            set
            {
                _overheating = value;
                Events_UIController.OverheatingChanged(_overheating, (weaponData as MachineGunData).MaxOverheating);
            }
        }

        bool canDropOverheating;

        float refrigerating = 1.5f;
        float timer;

        private void Update()
        {
            if (!canDropOverheating)
            {
                timer += Time.deltaTime;
                if(timer >= refrigerating)
                {
                    canDropOverheating = true;
                }
            }
            else
            {
                if (Overheating == 0)
                    return;

                Overheating -= Time.deltaTime * (weaponData as MachineGunData).OverheatingDropSpeed;
                if (Overheating <= 0)
                    Overheating = 0;
            }
        }

        public override void SingleShot(BulletData _bulletData, Transform _barrel)
        {
            if (Overheating < (weaponData as MachineGunData).MaxOverheating)
            {
                base.SingleShot(_bulletData, _barrel); 
            }
        }

        protected override void OnShot()
        {
            Overheating += (weaponData as MachineGunData).OverheatingPerShot;
            timer = 0;
            canDropOverheating = false;
        }
    }
}