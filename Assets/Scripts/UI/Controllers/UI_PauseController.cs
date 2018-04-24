using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamF
{
    public class UI_PauseController : MenuBase
    {
        public override void Init()
        {
            GameManager.I.UIMng.CurrentMenu = this;
            base.Init();
        }

        public override void Select()
        {
            switch (CurrentIndexSelected)
            {
                case 0:
                    GameManager.I.CurrentState = FlowState.Gameplay;
                    break;
                case 1:
                    GameManager.I.CurrentState = FlowState.MainMenu;
                    break;
                case 2:
                    GameManager.I.CurrentState = FlowState.QuitGame;
                    break;
            }
        }
    }
}