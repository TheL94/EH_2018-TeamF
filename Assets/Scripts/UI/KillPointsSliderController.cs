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

        Coroutine routine;

        private void Start()
        {
            Slider = GetComponent<Slider>();
        }

        public void Blink()
        {
            //if (routine != null)
            //    StopAllCoroutines();

            //routine = StartCoroutine(BlinkRoutine());
        }

        IEnumerator BlinkRoutine()
        {
            ChangeSliderImage();
            yield return new WaitForSeconds(.1f);
            ChangeSliderImage();
            routine = null;
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
