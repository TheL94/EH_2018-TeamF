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
        public GameObject NextRoundButton;

        public void Init(bool _isWin)
        {
            if (_isWin)
            {
                GameOverText.text = "Round Won";
                NextRoundButton.SetActive(true);
            }
            else
            {
                GameOverText.text = "Game Over";
                NextRoundButton.SetActive(false);
            }

            GameManager.I.UIMng.CurrentMenu = this;
            FindISelectableObects();
            SelectableButtons[0].IsSelected = true;
        }

        public override void Select()
        {
            switch (CurrentIndexSelected)
            {
                case 0:
                    // cambio stato in enter gameplay (*** il livellodeve essere già caricato ***)
                    GameManager.I.ChangeFlowState(FlowState.EnterGameplay);
                    break;
                case 1:
                    SceneManager.LoadScene(0);
                    break;
            }
        }
    }
}