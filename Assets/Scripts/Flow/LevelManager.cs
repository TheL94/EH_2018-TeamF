using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamF
{
    public class LevelManager
    {
        public LevelEndingStaus EndingStaus { get; private set; }
        public float PointsToWin { get; private set; }
        float roundPoints = 0;

        #region Constructor And Destructor
        public LevelManager(float _pointsToWin)
        {
            PointsToWin = _pointsToWin;
            Events_LevelController.OnKillPointChanged += UpdateRoundPoints;
        }

        ~LevelManager()
        {
            Events_LevelController.OnKillPointChanged -= UpdateRoundPoints;
        }
        #endregion

        #region Scene Management
        AsyncOperation async;
        int _level  = 0;
        public int Level { get { return _level; } set { OnLevelChange(value); } }
        void OnLevelChange(int _newLevel)
        {
            if (_level > 0)
            {
                async = SceneManager.UnloadSceneAsync(_level);
                async.completed += (async) =>
                {
                    Debug.Log("SceneUnload");
                };
            }

            if (_newLevel != _level && _newLevel != 0 && _newLevel < SceneManager.sceneCountInBuildSettings)
            {
                async = SceneManager.LoadSceneAsync(_newLevel, LoadSceneMode.Additive);
                async.completed += (async) =>
                {
                    _level = _newLevel;
                    async.allowSceneActivation = true;
                    GameManager.I.CurrentState++;
                };
            }
            else if (_newLevel >= SceneManager.sceneCountInBuildSettings)
            {
                _level = 0;
            }
        }
        #endregion

        public void OnUpdate()
        {
            if(async != null && async.progress != 0)
                Debug.Log(async.progress);
        }

        #region API
        public void UpdateRoundPoints(float _killedEnemyValue)
        {
            roundPoints += _killedEnemyValue;
            Events_UIController.KillPointsChanged(roundPoints, PointsToWin);

            CheckGameStatus();
        }

        public void CheckGameStatus()
        {
            if (roundPoints >= PointsToWin)
            {
                EndingStaus = LevelEndingStaus.Won;
                GameManager.I.CurrentState = FlowState.EndRound;
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
            EndingStaus = LevelEndingStaus.NotEnded;
        }
        #endregion
    }

    public enum LevelEndingStaus { NotEnded = 0, Won, Lost, Interrupted}
}

