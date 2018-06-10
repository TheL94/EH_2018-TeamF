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

            if (!Debug.isDebugBuild)
            {
                SelectableButton testSceneButton = (SelectableButtons[2] as SelectableButton);
                SelectableButtons.Remove(SelectableButtons[2]);
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
                    //ExitGame
                    GameManager.I.CurrentState = FlowState.QuitGame;
                    break;
                case 2:
                    //Scena test
                    GameManager.I.CurrentState = FlowState.InitTestScene;
                    break;
            }
            base.Select();
        }
    }
}