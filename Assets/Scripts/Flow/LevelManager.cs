using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamF
{
    public class LevelManager
    {
        public LevelEndingStaus EndingStaus { get; set; }

        float PointsToWin {
            get {
                if(Level > 0)
                    return GameManager.I.KillsToWinPerLevel[Level - 1];
                else
                    return GameManager.I.KillsToWinPerLevel[0];
            }
        }
        float roundPoints = 0;

        #region Constructor And Destructor
        public LevelManager()
        {
            Init();
        }

        public void Init()
        {
            Events_LevelController.OnKillPointChanged += UpdateRoundPoints;
        }
        #endregion

        #region Scene Management
        AsyncOperation async;
        public float LoadindProgress
        {
            get
            {
                if (async != null)
                    return async.progress;
                else
                    return -1f;
            }
        }

        int _level  = 0;
        public int Level { get { return _level; } set { OnLevelChange(value); } }

        void OnLevelChange(int _newLevel)
        {
            if (_level > 0)
            {
                SceneManager.UnloadSceneAsync(_level);
            }

            if (_newLevel != _level && _newLevel != 0 && _newLevel < SceneManager.sceneCountInBuildSettings)
            {
                async = SceneManager.LoadSceneAsync(_newLevel, LoadSceneMode.Additive);
                GameManager.I.UIMng.LoadingActions();
                async.completed += (async) =>
                {
                    _level = _newLevel;
                    async.allowSceneActivation = true;
                    if(GameManager.I.CurrentState == FlowState.ManageMap)
                        GameManager.I.CurrentState = FlowState.InitGameplayElements;
                    else if (GameManager.I.CurrentState == FlowState.InitTestScene)
                        GameManager.I.CurrentState = FlowState.TestGameplay;

                };
            }
            else if (_newLevel == 0 || _newLevel >= SceneManager.sceneCountInBuildSettings)
            {
                _level = 0;
                GameManager.I.CurrentState = FlowState.MainMenu;
            }
        }
        #endregion

        #region API
        public void UpdateRoundPoints(float _killedEnemyValue)
        {
            roundPoints += _killedEnemyValue;
            if (roundPoints >= PointsToWin)
                roundPoints = PointsToWin;
            Events_UIController.KillPointsChanged(roundPoints, PointsToWin);

            CheckGameStatus();
        }

        public void CheckGameStatus()
        {
            if (roundPoints == PointsToWin)
            {
                EndingStaus = LevelEndingStaus.Won;
                GameManager.I.CurrentState = FlowState.EndRound;
                Events_LevelController.OnKillPointChanged -= UpdateRoundPoints;

                return;
            }

            if(GameManager.I.Player.Character.Life <= 0)
            {
                EndingStaus = LevelEndingStaus.Lost;
                GameManager.I.CurrentState = FlowState.EndRound;
                return;
            }
        }

        public void ReInit()
        {
            roundPoints = 0;
            Events_UIController.KillPointsChanged(roundPoints, PointsToWin);
            EndingStaus = LevelEndingStaus.NotEnded;
        }
        #endregion
    }

    public enum LevelEndingStaus { NotEnded = 0, Won, Lost, Interrupted}
}

