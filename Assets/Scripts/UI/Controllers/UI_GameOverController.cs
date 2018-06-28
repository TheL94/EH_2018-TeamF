using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class UI_GameOverController : MenuBase
    {
        public GameObject NextRoundButton;
        public GameObject MainMenuButton;
        public Sprite WinImage;
        public Sprite LoseImage;

        public Text PartialScore;
        public Text TotalScore;
        public Text BestScore;

        private int _levelStatusInt; // 0 = won, 1 = fail

        public void Init(LevelEndingStaus _levelStaus)
        {
            Image img = GetComponent<Image>();
            switch (_levelStaus)
            {
                case LevelEndingStaus.Won:

                    if (GameManager.I.LevelMng.Level == GameManager.I.LevelMng.TotalLevels - 1)
                    {
                        NextRoundButton.SetActive(false);
                        MainMenuButton.SetActive(true);
                    }
                    else
                    {
                        NextRoundButton.SetActive(true);
                        MainMenuButton.SetActive(false);
                    }
                    img.sprite = WinImage;
                    _levelStatusInt = 0;
                    break;
                case LevelEndingStaus.Lost:
                    img.sprite = LoseImage;
                    NextRoundButton.SetActive(false);
                    MainMenuButton.SetActive(true);
                    _levelStatusInt = 1;
                    break;
            }

            GameManager.I.UIMng.CurrentMenu = this;
            base.Init();

            PartialScore.text = GameManager.I.ScoreCounter.LastPartialScore.ToString();
            TotalScore.text = GameManager.I.ScoreCounter.TotalScore.ToString();
            BestScore.text = GameManager.I.ScoreCounter.BestScore.ToString();
        }

        public override void Select()
        {
            switch (CurrentIndexSelected)
            {
                case 0:
                    if (_levelStatusInt == 0)
                    {
                        GameManager.I.CurrentState = FlowState.ManageMap;
                    }
                    else if (_levelStatusInt == 1)
                    {
                        GameManager.I.LevelMng.Level = 0; // Main Menù
                    }
                    
                    // cambio stato in enter gameplay (*** il livellodeve essere già caricato ***)
                    //if (SelectableButtons.Count > 1)
                        //GameManager.I.CurrentState = FlowState.ManageMap;
                    //else
                      //  GameManager.I.LevelMng.Level = 0; // Main Menù
                    break;
                case 1:
                    GameManager.I.LevelMng.Level = 0; // Main Menù
                    break;
            }
            base.Select();
        }
    }
}