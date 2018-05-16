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
        public Slider MachineGunOverhatingSlider;

        private void OnEnable()
        {
            Events_UIController.OnAmmoChanged += UpdateAmmo;
            Events_UIController.OnPointsChanged += UpdareKillPoints;
            Events_UIController.OnLifeChanged += UpdateLifeSlider;
            Events_UIController.OnOverheatingChanged += UpdateOverheatingSlider;

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
                        FireAmmo.text = "\u221E"; // simbolo dell'infinito
                    else
                        FireAmmo.text = _ammoValues.Ammo.ToString();                
                    break;
                case ElementalType.Water:
                    if (_ammoValues.Ammo < 0)
                        WaterAmmo.text = "\u221E"; // simbolo dell'infinito
                    else
                        WaterAmmo.text = _ammoValues.Ammo.ToString();
                    break;
                case ElementalType.Poison:
                    if (_ammoValues.Ammo < 0)
                        PoisonAmmo.text = "\u221E"; // simbolo dell'infinito
                    else
                        PoisonAmmo.text = _ammoValues.Ammo.ToString();
                    break;
                case ElementalType.Thunder:
                    if (_ammoValues.Ammo < 0)
                        ThunderAmmo.text = "\u221E"; // simbolo dell'infinito
                    else
                        ThunderAmmo.text = _ammoValues.Ammo.ToString();
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

        void UpdateOverheatingSlider(float _value, float _totalOverheating)
        {
            MachineGunOverhatingSlider.value = _value / _totalOverheating;
        }

        private void OnDisable()
        {
            Events_UIController.OnAmmoChanged -= UpdateAmmo;
            Events_UIController.OnPointsChanged -= UpdareKillPoints;
            Events_UIController.OnLifeChanged -= UpdateLifeSlider;
            ComboCounter.OnCounterChanged -= UpdateComboCounter;
            Events_UIController.OnOverheatingChanged -= UpdateOverheatingSlider;
        }
    }
}
