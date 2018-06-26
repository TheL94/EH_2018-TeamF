using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class MachineGunSliderController : MonoBehaviour
    {
        [HideInInspector]
        public Slider Slider;
        public Image SliderImage;
        public List<Sprite> Images;

        bool _isHot;
        bool IsHot
        {
            get { return _isHot; }
            set
            {
                if (_isHot != value)
                {
                    _isHot = value;
                    if (_isHot)
                        StartCoroutine(Blink());
                    else
                    {
                        StopAllCoroutines();
                        ChangeSliderImage(0);
                    }
                }
            }
        }

        protected void Start()
        {
            Slider = GetComponent<Slider>();
        }

        public void UpdateSlider(float _value, float _totalOverheating)
        {
            float sliderValue = Slider.value = _value / _totalOverheating;
            if (sliderValue >= .75f)
                IsHot = true;
            else
                IsHot = false;
        }

        IEnumerator Blink()
        {
            yield return new WaitForSeconds(.2f);
            ChangeSliderImage();
            StartCoroutine(Blink());
        }

        int selectedImage = 0;
        void ChangeSliderImage()
        {
            if (selectedImage == 0)
                selectedImage++;
            else
                selectedImage = 0;

            SliderImage.sprite = Images[selectedImage];
        }

        void ChangeSliderImage(int _imageToLoad)
        {
            selectedImage = _imageToLoad;
            SliderImage.sprite = Images[selectedImage];
        }

    }
}

