using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class UI_GameplayController : MonoBehaviour
    {
        public AmmoIndicator FireAmmo;
        public AmmoIndicator WaterAmmo;
        public AmmoIndicator PoisonAmmo;
        public AmmoIndicator ThunderAmmo;
        public Text ComboCounterText;
        public Text ScoreText;

        public Slider KillPointsSlider;
        public Slider LifeSlider;
        public MachineGunSliderController MachineGunOverhatingCtrl;

        private void OnEnable()
        {
            Events_UIController.OnAmmoChanged += UpdateAmmo;
            Events_UIController.OnPointsChanged += UpdareKillPoints;
            Events_UIController.OnLifeChanged += UpdateLifeSlider;
            Events_UIController.OnOverheatingChanged += UpdateOverheatingSlider;

            ComboCounter.OnCounterChanged += UpdateComboCounter;
            ScoreCounter.OnScoreChange += UpdateScore;
        }

        public void Init()
        {
            UpdateScore(0);
            UpdateComboCounter(0);
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
                        FireAmmo.SetAmmoCount("\u221E"); // simbolo dell'infinito
                    else
                        FireAmmo.SetAmmoCount(_ammoValues.Ammo.ToString());                
                    break;
                case ElementalType.Water:
                    if (_ammoValues.Ammo < 0)
                        WaterAmmo.SetAmmoCount("\u221E"); // simbolo dell'infinito
                    else
                        WaterAmmo.SetAmmoCount(_ammoValues.Ammo.ToString());
                    break;
                case ElementalType.Poison:
                    if (_ammoValues.Ammo < 0)
                        PoisonAmmo.SetAmmoCount("\u221E"); // simbolo dell'infinito
                    else
                        PoisonAmmo.SetAmmoCount(_ammoValues.Ammo.ToString());
                    break;
                case ElementalType.Thunder:
                    if (_ammoValues.Ammo < 0)
                        ThunderAmmo.SetAmmoCount("\u221E"); // simbolo dell'infinito
                    else
                        ThunderAmmo.SetAmmoCount(_ammoValues.Ammo.ToString());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Attiva l'immagine per le munizioni selezionate
        /// </summary>
        /// <param name="_type">Il tipo delle munizioni selezionate</param>
        public void UpdateSelectedAmmo(ElementalAmmo _ammoType)                            //TODO: collegare la funzione ad un evento richiamato quando il character cambia munizione
        {
            FireAmmo.IsCurrentSelected(false);
            WaterAmmo.IsCurrentSelected(false);
            PoisonAmmo.IsCurrentSelected(false);
            ThunderAmmo.IsCurrentSelected(false);

            switch (_ammoType.AmmoType)
            {
                case ElementalType.Fire:
                    FireAmmo.IsCurrentSelected(true);
                    break;
                case ElementalType.Water:
                WaterAmmo.IsCurrentSelected(true);
                    break;
                case ElementalType.Poison:
                PoisonAmmo.IsCurrentSelected(true);
                    break;
                case ElementalType.Thunder:
                ThunderAmmo.IsCurrentSelected(true);
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
            MachineGunOverhatingCtrl.UpdateSlider(_value, _totalOverheating);
        }

        void UpdateScore(int _score)
        {
            ScoreText.text = "Score: " + _score;
        }

        private void OnDisable()
        {
            Events_UIController.OnAmmoChanged -= UpdateAmmo;
            Events_UIController.OnPointsChanged -= UpdareKillPoints;
            Events_UIController.OnLifeChanged -= UpdateLifeSlider;
            ComboCounter.OnCounterChanged -= UpdateComboCounter;
            Events_UIController.OnOverheatingChanged -= UpdateOverheatingSlider;
            ScoreCounter.OnScoreChange -= UpdateScore;

        }
    }
}
