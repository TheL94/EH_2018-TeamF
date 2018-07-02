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

        public int OrangeText = 3;
        public int RedText = 6;

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

                if(_counter > OrangeText && _counter < RedText)
                    ComboImage.sprite = Images[1];
                else if (_counter > RedText)
                    ComboImage.sprite = Images[2];

                ComboCounterText.text = "x" + _counter.ToString();
            }
        }
    }
}