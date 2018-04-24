using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class UI_GameOverController : MenuBase
    {
        public Text GameOverText;
        public GameObject NextRoundButton;

        public void Init(LevelEndingStaus _levelStaus)
        {
            switch (_levelStaus)
            {
                case LevelEndingStaus.Won:
                    GameOverText.text = "Round Won";
                    NextRoundButton.SetActive(true);
                    break;
                case LevelEndingStaus.Lost:
                    GameOverText.text = "Game Over";
                    NextRoundButton.SetActive(false);
                    break;
            }

            GameManager.I.UIMng.CurrentMenu = this;
            base.Init();
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
                        GameManager.I.CurrentState = FlowState.MainMenu;
                    break;
                case 1:
                    GameManager.I.CurrentState = FlowState.MainMenu;
                    break;
            }
        }
    }
}