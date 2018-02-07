using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class UI_GameplayController : MonoBehaviour
    {

        public Text FireAmmo;
        public Text WaterAmmo;
        public Text PoisonAmmo;
        public Text ThunderAmmo;

        private void OnEnable()
        {
            EventManager.OnAmmoChanged += UpdateAmmo;
        }

        public void UpdateAmmo(ElementalAmmo _ammoValues)
        {
            switch (_ammoValues.AmmoType)
            {
                case AmmoType.Fire:
                    FireAmmo.text = "1 - Fire: " + _ammoValues.Ammo;
                    break;
                case AmmoType.Water:
                    WaterAmmo.text = "2 - Water: " + _ammoValues.Ammo;
                    break;
                case AmmoType.Poison:
                    PoisonAmmo.text = "3 - Poison: " + _ammoValues.Ammo;
                    break;
                case AmmoType.Thunder:
                    ThunderAmmo.text = "4 - Thunder: " + _ammoValues.Ammo;
                    break;
                default:
                    print("Default");
                    break;
            }
        }

        private void OnDisable()
        {
            EventManager.OnAmmoChanged -= UpdateAmmo;
        }

    }
}
