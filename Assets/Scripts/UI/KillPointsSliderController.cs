using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class KillPointsSliderController : MonoBehaviour
    {
        [HideInInspector]
        public Slider Slider;
        public Image SliderImage;
        public List<Sprite> Images;

        int coroutineCount;

        private void Start()
        {
            Slider = GetComponent<Slider>();
        }

        public void Blink()
        {
            if (coroutineCount > 0)
            {
                StopAllCoroutines();
                ChangeSliderImage(0);
                coroutineCount--;
            }

            StartCoroutine(BlinkRoutine());
        }

        IEnumerator BlinkRoutine()
        {
            coroutineCount++;
            ChangeSliderImage(1);
            yield return new WaitForSeconds(.1f);
            ChangeSliderImage(0);
            coroutineCount--;
        }

        int selectedImage = 0;
        void ChangeSliderImage(int _imageToLoad)
        {
            selectedImage = _imageToLoad;
            SliderImage.sprite = Images[selectedImage];
        }
    }
}
