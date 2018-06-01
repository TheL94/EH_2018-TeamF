using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class AmmoIndicator : MonoBehaviour
    {
        public Text AmmoText;
        public Image SelectedImage;

        /// <summary>
        /// Attiva o disattiva l'immagine per indicare se la munizione è selezionata
        /// </summary>
        /// <param name="_isActive">Lo stato che deve avere</param>
        public void IsCurrentSelected(bool _isActive)
        {
            if (_isActive)
                SelectedImage.enabled = true;
            else
                SelectedImage.enabled = false;
        }

        /// <summary>
        /// Scrive la quantità di munizioni
        /// </summary>
        /// <param name="_ammoCount">La quantità di munizioni</param>
        public void SetAmmoCount(string _ammoCount)
        {
            AmmoText.text = _ammoCount;
        }
    }
}