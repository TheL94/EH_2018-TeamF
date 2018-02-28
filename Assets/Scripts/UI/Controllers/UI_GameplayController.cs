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

        public Slider KillPointsSlider;
        public Slider LifeSlider;

        private void OnEnable()
        {
            Events_UIController.OnAmmoChanged += UpdateAmmo;
            Events_UIController.OnPointsChanged += UpdareKillPoints;
            Events_UIController.OnLifeChanged += UpdateLifeSlider;
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

        void UpdateLifeSlider(float _life, float _totalLife)
        {
            LifeSlider.value = _life / _totalLife;
        }

        private void OnDisable()
        {
            Events_UIController.OnAmmoChanged -= UpdateAmmo;
            Events_UIController.OnPointsChanged -= UpdareKillPoints;
            Events_UIController.OnLifeChanged -= UpdateLifeSlider;
        }

    }
}
