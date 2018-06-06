using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class UI_LoadingPanelController : MonoBehaviour
    {
        public Text LoadingText;

        public float RepeatTime;
        float timer;

        string previusText;
        string currentText = "Loading";

        private void Start()
        {
            LoadingText.text = currentText;
        }

        void Update()
        {
            timer += Time.deltaTime;
            if(timer >= RepeatTime)
            {
                LoadingText.text = AnimatedLoading();
                timer = 0;
            }
        }

        string AnimatedLoading()
        {
            previusText = currentText;
            if (previusText.Contains("..."))
                currentText = "Loading";
            else if (previusText.Contains(".."))
                currentText = "Loading...";
            else if (previusText.Contains("."))
                currentText = "Loading..";
            else
                currentText = "Loading.";

            return currentText;
        }
    }
}
