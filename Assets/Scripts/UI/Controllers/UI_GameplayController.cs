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
        public Text ComboCounterText;


        public Slider KillPointsSlider;
        public Slider LifeSlider;

        private void OnEnable()
        {
            Events_UIController.OnAmmoChanged += UpdateAmmo;
            Events_UIController.OnPointsChanged += UpdareKillPoints;
            Events_UIController.OnLifeChanged += UpdateLifeSlider;
            ComboCounter.OnCounterChanged += UpdateComboCounter;
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
                    if (_ammoValues.Ammo < 0)
                        FireAmmo.text = "Fire: \u221E"; // simbolo dell'infinito
                    else
                        FireAmmo.text = "Fire: " + _ammoValues.Ammo;                
                    break;
                case ElementalType.Water:
                    if (_ammoValues.Ammo < 0)
                        WaterAmmo.text = "Fire: \u221E"; // simbolo dell'infinito
                    else
                        WaterAmmo.text = "Water: " + _ammoValues.Ammo;
                    break;
                case ElementalType.Poison:
                    if (_ammoValues.Ammo < 0)
                        PoisonAmmo.text = "Fire: \u221E"; // simbolo dell'infinito
                    else
                        PoisonAmmo.text = "Poison: " + _ammoValues.Ammo;
                    break;
                case ElementalType.Thunder:
                    if (_ammoValues.Ammo < 0)
                        ThunderAmmo.text = "Fire: \u221E"; // simbolo dell'infinito
                    else
                        ThunderAmmo.text = "Thunder: " + _ammoValues.Ammo;
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

        void UpdateComboCounter(int _counter)
        {
            if (_counter == 0)
                ComboCounterText.text = string.Empty;
            else
                ComboCounterText.text = "+" + _counter.ToString();
        }

        private void OnDisable()
        {
            Events_UIController.OnAmmoChanged -= UpdateAmmo;
            Events_UIController.OnPointsChanged -= UpdareKillPoints;
            Events_UIController.OnLifeChanged -= UpdateLifeSlider;
            ComboCounter.OnCounterChanged -= UpdateComboCounter;

        }
    }
}
