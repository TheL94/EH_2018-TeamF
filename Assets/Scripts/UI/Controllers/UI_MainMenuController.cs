using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class UI_MainMenuController : MenuBase
    {
        public Text BestScore;

        public override void Init()
        {
            base.Init();
            GameManager.I.UIMng.CurrentMenu = this;

            BestScore.text = "Best Score: " + GameManager.I.ScoreCounter.BestScore;

            if (!Debug.isDebugBuild && SelectableButtons.Count > 5)
            {
                SelectableButton testSceneButton = (SelectableButtons[SelectableButtons.Count - 1] as SelectableButton);
                SelectableButtons.Remove(SelectableButtons[SelectableButtons.Count - 1]);
                Destroy(testSceneButton.gameObject);
            }
        }

        public override void Select()
        {
            switch (CurrentIndexSelected)
            {
                case 0:
                    //Start game;
                    GameManager.I.CurrentState = FlowState.ManageMap;
                    break;
                case 1:
                    //Pannello Crediti
                    GameManager.I.UIMng.CreditsActions();
                    break;
                case 2:
                    //Tutorial
                    GameManager.I.UIMng.TutorialActions();
                    break;
                case 3:
                    //Lore
                    GameManager.I.UIMng.LoreActions();
                    break;
                case 4:
                    //Exit game
                    GameManager.I.CurrentState = FlowState.QuitGame;
                    break;
                case 5:
                    //Scena di test
                    GameManager.I.CurrentState = FlowState.InitTestScene;
                    break;
            }
            base.Select();
        }
    }
}