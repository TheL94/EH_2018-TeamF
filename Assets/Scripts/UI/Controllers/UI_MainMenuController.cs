using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class UI_MainMenuController : MenuBase
    {
        public void Init()
        {
            GameManager.I.UIMng.CurrentMenu = this;
            FindISelectableObects();
            SelectableButtons[0].IsSelected = true;
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
                    //Scena test
                    GameManager.I.CurrentState = FlowState.InitTestScene;                   
                    break;
                case 2:
                    //ExitGame;
                    GameManager.I.CurrentState = FlowState.QuitGame;
                    break;
            }
        }
    }
}