using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace TeamF
{
    public class LevelManager
    {
        public float PointsToWin { get; private set; }
        float roundPoints = 0;

        private int _level = 1;

        public int Level
        {
            get { return _level; }
            private set
            {
                SceneManager.UnloadSceneAsync(_level);
                CheckSceneInGame();
                _level = value;
                SceneManager.LoadScene(_level, LoadSceneMode.Additive);
            }
        }

        #region API
        public LevelManager(float _pointsToWin)
        {
            PointsToWin = _pointsToWin;
            Events_LevelController.OnKillPointChanged += UpdateRoundPoints;
            CheckSceneInGame();
            SceneManager.LoadScene(Level, LoadSceneMode.Additive);
        }

        public void UpdateRoundPoints(float _killedEnemyValue)
        {
            roundPoints += _killedEnemyValue;
            Events_UIController.KillPointsChanged(roundPoints, PointsToWin);

            if(CheckVictory())   
                GoToGameWon();
        }

        public void GoToGameWon()
        {
            GameManager.I.ChangeFlowState(FlowState.GameWon);
        }

        public void GoToGameLost()
        {
            GameManager.I.ChangeFlowState(FlowState.GameLost);
        }

        public void UpdateLevel()
        {
            roundPoints = 0;
            if(Level < SceneManager.sceneCountInBuildSettings)
                Level++;
        }
        #endregion

        void CheckSceneInGame()
        {
            if(SceneManager.sceneCount > 1)
            {
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    if (i == 0)
                        return;
                    SceneManager.UnloadSceneAsync(i);
                }
            }
        }

        bool CheckVictory()
        {
            if (roundPoints >= PointsToWin)
                return true;

            return false;
        }

        ~ LevelManager()
        {
            Events_LevelController.OnKillPointChanged -= UpdateRoundPoints;
        }
    }
}

