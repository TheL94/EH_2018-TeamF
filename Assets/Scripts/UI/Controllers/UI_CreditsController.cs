using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class UI_CreditsController : MenuBase
    {

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
                    GameManager.I.UIMng.MainMenuActions();
                    break;
            }
        }

    }
}