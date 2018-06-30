using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class UI_TutorialController : MenuBase
    {
        Image tutorialImage;
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

                tutorialImage.sprite = TutorialImages[_currentImg];
            }
        }

        public override void Init()
        {
            base.Init();
            tutorialImage = GetComponent<Image>();
            GameManager.I.UIMng.CurrentMenu = this;
            CurrentImg = 0;
        }

        public override void Select()
        {
            switch (CurrentIndexSelected)
            {
                case 0:
                    // go next
                    GoRightInMenu();
                    break;
                case 1:
                    // go previous
                    GoLeftInMenu();
                    break;
                case 2:
                    GoBack();
                    break;
            }
        }

        public override void GoDownInMenu()
        {
            // per inibire la possibilità di spostare selectable
        }
        public override void GoUpInMenu()
        {
            // per inibire la possibilità di spostare selectable
        }

        public override void GoLeftInMenu()
        {
            CurrentImg--;
        }

        public override void GoRightInMenu()
        {
            CurrentImg++;
        }

        public override void GoBack()
        {
            // Exit
            GameManager.I.UIMng.MainMenuActions();
        }
    }
}