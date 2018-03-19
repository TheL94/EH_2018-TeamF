using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class MachineGun : Weapon
    {
        public float Overheating;
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
                    timer = 0;
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

        public override void SingleShot(ElementalAmmo _selectedAmmo)
        {
            if (Overheating < (weaponData as MachineGunData).MaxOverheating)
            {
                base.SingleShot(_selectedAmmo); 
            }
        }

        protected override void OnShot()
        {
            Overheating += (weaponData as MachineGunData).OverheatingPerShot;
            canDropOverheating = false;
        }
    }
}