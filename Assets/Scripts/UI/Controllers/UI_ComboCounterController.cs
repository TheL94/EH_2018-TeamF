using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class UI_ComboCounterController : MonoBehaviour
    {
        public Text ComboCounterText;
        public Image ComboImage;
        public List<Sprite> Images;

        public void UpdateCounter(int _counter)
        {
            if (_counter == 0)
            {
                ComboCounterText.text = string.Empty;
                ComboImage.sprite = Images[0];
                ComboImage.enabled = false;
            }
            else
            {
                ComboImage.enabled = true;

                if(_counter > 3 && _counter < 6)
                    ComboImage.sprite = Images[1];
                else if (_counter > 6)
                    ComboImage.sprite = Images[2];

                ComboCounterText.text = "+" + _counter.ToString();
            }
        }
    }
}