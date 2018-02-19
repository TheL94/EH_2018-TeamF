using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class UI_MainMenuController : MenuBase, IButtonController
    {
        

        public void Init()
        {
            GameManager.I.UIMng.CurrentMenu = this;
            FindISelectableObects();
            SetSelectablesController();
            SelectableButtons[0].IsSelected = true;
        }

        public override void Select()
        {
            switch (CurrentIndexSelected)
            {
                case 0:
                    //Start game;
                    GameManager.I.ChangeFlowState(FlowState.EnterGameplay);
                    break;
                case 1:
                    //ExitGame;
                    GameManager.I.CloseApplicationActions();
                    break;
            }
        }

        public void ButtonClick(int _buttonID)
        {
            CurrentIndexSelected = _buttonID;
            Select();
        }

        public void SetSelectablesController()
        {
            for (int i = 0; i < SelectableButtons.Count; i++)
            {
                SelectableButtons[i].Controller = this;
            }
        }
    }
}