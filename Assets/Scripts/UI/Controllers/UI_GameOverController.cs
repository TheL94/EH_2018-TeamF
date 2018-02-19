using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace TeamF
{
    public class UI_GameOverController : MenuBase, IButtonController
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
            SetSelectablesController();
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