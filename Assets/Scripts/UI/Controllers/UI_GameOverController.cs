using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class UI_GameOverController : MenuBase
    {
        public GameObject NextRoundButton;
        public Sprite WinImage;
        public Sprite LoseImage;

        public Text PartialScore;
        public Text TotalScore;

        public void Init(LevelEndingStaus _levelStaus)
        {
            Image img = GetComponent<Image>();
            switch (_levelStaus)
            {
                case LevelEndingStaus.Won:

                    if (GameManager.I.LevelMng.Level == GameManager.I.LevelMng.TotalLevels - 1)
                        NextRoundButton.SetActive(false);
                    else
                        NextRoundButton.SetActive(true);

                    img.sprite = WinImage;
                    break;
                case LevelEndingStaus.Lost:
                    img.sprite = LoseImage;
                    NextRoundButton.SetActive(false);
                    break;
            }

            GameManager.I.UIMng.CurrentMenu = this;
            base.Init();

            PartialScore.text = GameManager.I.ScoreCounter.LastPartialScore.ToString();
            TotalScore.text = GameManager.I.ScoreCounter.TotalScore.ToString();
        }

        public override void Select()
        {
            switch (CurrentIndexSelected)
            {
                case 0:
                    // cambio stato in enter gameplay (*** il livellodeve essere già caricato ***)
                    if (SelectableButtons.Count > 1)
                        GameManager.I.CurrentState = FlowState.ManageMap;
                    else
                        GameManager.I.LevelMng.Level = 0; // Main Menù
                    break;
                case 1:
                    GameManager.I.LevelMng.Level = 0; // Main Menù
                    break;
            }
            base.Select();
        }
    }
}