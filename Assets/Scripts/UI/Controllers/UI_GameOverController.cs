using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace TeamF
{
    public class UI_GameOverController : MenuBase
    {
        public Text GameOverText;

        public void Init(bool _isWin)
        {
            if (_isWin)
                GameOverText.text = "Round Won";
            else
                GameOverText.text = "Game Over";

            GameManager.I.UIMng.CurrentMenu = this;
            FindISelectableObects();
            SelectableButtons[0].IsSelected = true;
        }

        public override void Select()
        {
            switch (CurrentIndexSelected)
            {
                case 0:
                    //GameManager.I.ChangeFlowState(FlowState.Menu);
                    SceneManager.LoadScene(0);
                    break;
            }
        }
    }
}