using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class UI_LoadingPanelController : MonoBehaviour
    {
        public Text LoadingText;

        void Update()
        {
            if (GameManager.I.LevelMng.LoadindProgress != -1f)
                LoadingText.text = GameManager.I.LevelMng.LoadindProgress.ToString() + " %";
            else
                LoadingText.text = "0 %";
        }
    }
}
