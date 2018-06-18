using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamF
{
    public class LevelManager : MonoBehaviour
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
        public int TotalLevels { get { return SceneManager.sceneCountInBuildSettings; } }

        public void Init()
        {
            Events_LevelController.OnKillPointChanged += UpdateRoundPoints;
        }

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
            GameManager.I.UIMng.LoadingActions();
            UnloadLevel(Level, _newLevel);
        }

        void UnloadLevel(int _currentLevel, int _newLevel)
        {
            if (_currentLevel > 0)
                StartCoroutine(DectivateScene(_currentLevel, _newLevel));
            else
                LoadNewLevel(_newLevel);
        }

        IEnumerator DectivateScene(int _currentLevel, int _newLevel)
        {
            while (!GameManager.I.EnemyMng.IsFreeToGo)
                yield return null;

            async = SceneManager.UnloadSceneAsync(_currentLevel);
            if(async != null)
            {
                async.completed += (async) =>
                {
                    LoadNewLevel(_newLevel);
                };
            }
        }

        void LoadNewLevel(int _newLevel)
        {
            if (_newLevel != _level && _newLevel != 0 && _newLevel < TotalLevels)
            {
                async = SceneManager.LoadSceneAsync(_newLevel, LoadSceneMode.Additive);
                async.completed += (async) =>
                {
                    StartCoroutine(ActivateScene(_newLevel));
                };
            }
            else if (_newLevel == 0 || _newLevel >= TotalLevels)
            {
                _level = 0;
                GameManager.I.CurrentState = FlowState.MainMenu;
            }
        }

        IEnumerator ActivateScene(int _newLevel)
        {
            while (!GameManager.I.PoolMng.IsFreeToGo)
                yield return null;

            _level = _newLevel;
            async.allowSceneActivation = true;
            if (GameManager.I.CurrentState == FlowState.ManageMap)
                GameManager.I.CurrentState = FlowState.InitGameplayElements;
            else if (GameManager.I.CurrentState == FlowState.InitTestScene)
                GameManager.I.CurrentState = FlowState.TestGameplay;
        }
        #endregion

        #region API

        public void CheckGameStatus()
        {
            if (roundPoints == PointsToWin)
            {
                EndingStaus = LevelEndingStaus.Won;
                GameManager.I.CurrentState = FlowState.PreEndRound;
                Events_LevelController.OnKillPointChanged -= UpdateRoundPoints;

                return;
            }

            if(GameManager.I.Player.Character.Life <= 0)
            {
                EndingStaus = LevelEndingStaus.Lost;
                GameManager.I.CurrentState = FlowState.PreEndRound;
                Events_LevelController.OnKillPointChanged -= UpdateRoundPoints;
                return;
            }
        }

        public void ReInit()
        {
            roundPoints = 0;
            Events_UIController.KillPointsChanged(roundPoints, PointsToWin);
            EndingStaus = LevelEndingStaus.NotEnded;
        }

        [HideInInspector]
        public List<ElementalComboBase> Combos = new List<ElementalComboBase>();
        public void ClearCombos() 
        {
            for (int i = 0; i < Combos.Count; i++)
                Combos[i].EndEffect();
        }
        #endregion

        void UpdateRoundPoints(float _killedEnemyValue)
        {
            roundPoints += _killedEnemyValue;
            if (roundPoints >= PointsToWin)
                roundPoints = PointsToWin;
            Events_UIController.KillPointsChanged(roundPoints, PointsToWin);

            CheckGameStatus();
        }

    }

    public enum LevelEndingStaus { NotEnded = 0, Won, Lost, Interrupted }
}

