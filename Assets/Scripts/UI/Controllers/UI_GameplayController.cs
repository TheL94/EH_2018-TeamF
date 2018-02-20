﻿using System.Collections;
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

        public Slider KillPointsSlider;

        private void OnEnable()
        {
            EventManager.OnAmmoChanged += UpdateAmmo;
            EventManager.OnPointsChanged += UpdareKillPoints;
        }

        /// <summary>
        /// Funzione per Aggiornare il numero di munizioni in UI
        /// </summary>
        /// <param name="_ammoValues"></param>
        void UpdateAmmo(ElementalAmmo _ammoValues)
        {
            switch (_ammoValues.AmmoType)
            {
                case ElementalType.Fire:
                    FireAmmo.text = "2 - Fire: " + _ammoValues.Ammo;
                    break;
                case ElementalType.Water:
                    WaterAmmo.text = "3 - Water: " + _ammoValues.Ammo;
                    break;
                case ElementalType.Poison:
                    PoisonAmmo.text = "4 - Poison: " + _ammoValues.Ammo;
                    break;
                case ElementalType.Thunder:
                    ThunderAmmo.text = "5 - Thunder: " + _ammoValues.Ammo;
                    break;
                default:
                    break;
            }
        }

        void UpdareKillPoints(float _points, float _pointsToWin)
        {
            KillPointsSlider.value = _points / _pointsToWin;
        }

        private void OnDisable()
        {
            EventManager.OnAmmoChanged -= UpdateAmmo;
            EventManager.OnPointsChanged -= UpdareKillPoints;

        }

    }
}
