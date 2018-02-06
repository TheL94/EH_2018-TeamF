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


        public void UpdateAmmo(ElementalAmmo _ammoValues, AmmoType _type)
        {
            switch (_type)
            {
                case AmmoType.Fire:
                    FireAmmo.text = "Fire: " + _ammoValues.FireAmmo;
                    break;
                case AmmoType.Water:
                    WaterAmmo.text = "Water: " + _ammoValues.WaterAmmo;
                    break;
                case AmmoType.Poison:
                    PoisonAmmo.text = "Poison: " + _ammoValues.PoisonAmmo;
                    break;
                case AmmoType.Thunder:
                    ThunderAmmo.text = "Thunder: " + _ammoValues.ThunderAmmo;
                    break;
                default:
                    break;
            }
        }

    }
}
