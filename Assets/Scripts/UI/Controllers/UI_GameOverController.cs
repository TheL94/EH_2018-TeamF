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

        LevelEndingStaus levelEndingStaus;

        public void Init(LevelEndingStaus _levelStaus)
        {
            Image img = GetComponent<Image>();
            switch (_levelStaus)
            {
                case LevelEndingStaus.Won:
                    if (GameManager.I.LevelMng.MapIndex == GameManager.I.LevelMng.TotalMaps - 1)
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
                    levelEndingStaus = _levelStaus;
                    break;
                case LevelEndingStaus.Lost:
                    img.sprite = LoseImage;
                    NextRoundButton.SetActive(false);
                    MainMenuButton.SetActive(true);
                    levelEndingStaus = _levelStaus;
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
                    if (levelEndingStaus == LevelEndingStaus.Won)
                        GameManager.I.CurrentState = FlowState.ManageMap; // Next map
                    else if (levelEndingStaus == LevelEndingStaus.Lost)
                        GameManager.I.LevelMng.MapIndex = 0; // Main Menù
                    break;
            }
            base.Select();
        }
    }
}