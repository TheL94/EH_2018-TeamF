using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class UI_LoreController : MenuBase
    {
        public Image LoreImage;
        public List<Sprite> LoreImages = new List<Sprite>();

        private int _currentImg;

        int CurrentImg
        {
            get { return _currentImg; }
            set {
                if (value >= LoreImages.Count)
                    _currentImg = 0;
                else if(value < 0)
                    _currentImg = LoreImages.Count - 1;
                else
                    _currentImg = value;

                ChangeImage(_currentImg);
            }
        }

        public override void Init()
        {
            base.Init();
            GameManager.I.UIMng.CurrentMenu = this;
        }

        public override void Select()
        {
            switch (CurrentIndexSelected)
            {
                case 0:
                    // go next
                    CurrentImg++;
                    break;
                case 1:
                    // go previous
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
            LoreImage.sprite = LoreImages[_indexImg];
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
            // per inibire la possibilità di spostare selectable
        }

        public override void GoRightInMenu()
        {
            // per inibire la possibilità di spostare selectable
        }
    }
}