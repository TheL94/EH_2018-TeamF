using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class UI_TutorialController : MenuBase
    {
        Image img;
        public List<Sprite> TutorialImages = new List<Sprite>();

        private int _currentImg;

        int CurrentImg
        {
            get { return _currentImg; }
            set {
                if (value >= TutorialImages.Count)
                    _currentImg = 0;
                else if(value < 0)
                    _currentImg = TutorialImages.Count - 1;
                else
                    _currentImg = value;

                ChangeImage(_currentImg);
            }
        }


        public override void Init()
        {
            base.Init();
            img = GetComponent<Image>();
            GameManager.I.UIMng.CurrentMenu = this;
        }

        public override void Select()
        {
            switch (CurrentIndexSelected)
            {
                case 0:
                    // Go next image
                    CurrentImg++;
                    break;
                case 1:
                    // Go prev image
                    CurrentImg--;
                    break;
                case 2:
                    // Exit
                    GameManager.I.UIMng.MainMenuActions();
                    break;
            }
        }

        void ChangeImage(int _indexImg)
        {
            img.sprite = TutorialImages[_indexImg];
        }
    }
}