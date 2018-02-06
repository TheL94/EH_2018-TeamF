using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamF
{
    public class UIManager : MonoBehaviour
    {

        public UI_GameplayController UI_GameplayCtrl;
        public UI_MainMenuController UI_MainMenuCtrl;
        public UI_GameOverController UI_GameOverCtrl;

        [HideInInspector]
        public MenuBase CurrentMenu;

        public void MainMenuActions()
        {
            UI_MainMenuCtrl.gameObject.SetActive(true);
            UI_GameOverCtrl.gameObject.SetActive(false);
            UI_MainMenuCtrl.Init();
        }

        public void GameplayActions()
        {
            UI_MainMenuCtrl.gameObject.SetActive(false);
            UI_GameOverCtrl.gameObject.SetActive(false);
        }

        public void GameOverActions()
        {
            UI_MainMenuCtrl.gameObject.SetActive(false);
            UI_GameOverCtrl.gameObject.SetActive(true);
            UI_GameOverCtrl.Init();
        }
    }
}