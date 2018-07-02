using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.Pool;

namespace TeamF
{
    public class FlowManager
    {
        private FlowState _currentState = FlowState.None;
        public FlowState CurrentState
        {
            get { return _currentState; }
            set
            {
                if (_currentState != value)
                    OnStateChange(value, _currentState);
            }
        }

        private void OnStateChange(FlowState _newState, FlowState _oldState)
        {
            switch (_newState)
            {
                #region Regular Flow
                case FlowState.SetupGame:
                    if (_oldState == FlowState.None)
                    {
                        _currentState = _newState;
                        SetupGameActions();
                    }
                    else
                    {
                        Debug.LogWarning("Passaggio di stato non permesso da : " + _oldState + " a : " + _newState);
                    }
                    break;

                case FlowState.MainMenu:
                    if (_oldState == FlowState.SetupGame || _oldState == FlowState.EndRound || _oldState == FlowState.ManageMap || _oldState == FlowState.ExitTestScene)
                    {
                        _currentState = _newState;
                        MainMenuActions();
                    }
                    else
                    {
                        Debug.LogWarning("Passaggio di stato non permesso da : " + _oldState + " a : " + _newState);
                    }
                    break;

                case FlowState.ManageMap:
                    if (_oldState == FlowState.MainMenu || _oldState == FlowState.EndRound || _oldState == FlowState.Gameplay || _oldState == FlowState.InitGameplayElements)
                    {
                        _currentState = _newState;
                        ManageMapActions();
                    }
                    else
                    {
                        Debug.LogWarning("Passaggio di stato non permesso da : " + _oldState + " a : " + _newState);
                    }
                    break;

                case FlowState.InitGameplayElements:
                    if (_oldState == FlowState.ManageMap)
                    {
                        _currentState = _newState;
                        InitGameplayElementsActions();
                    }
                    else
                    {
                        Debug.LogWarning("Passaggio di stato non permesso da : " + _oldState + " a : " + _newState);
                    }
                    break;

                case FlowState.Gameplay:
                    if (_oldState == FlowState.InitGameplayElements || _oldState == FlowState.Pause)
                    {
                        _currentState = _newState;
                        if (_oldState == FlowState.Pause)
                            PauseActions(false);
                        else
                            GameplayActions();
                    }
                    else
                    {
                        Debug.LogWarning("Passaggio di stato non permesso da : " + _oldState + " a : " + _newState);
                    }
                    break;

                case FlowState.Pause:
                    if (_oldState == FlowState.Gameplay)
                    {
                        _currentState = _newState;
                        PauseActions(true);
                    }
                    else
                    {
                        Debug.LogWarning("Passaggio di stato non permesso da : " + _oldState + " a : " + _newState);
                    }
                    break;

                case FlowState.PreEndRound:
                    if (_oldState == FlowState.Gameplay)
                    {
                        _currentState = _newState;
                        PreRoundEndActions();
                    }
                    else
                    {
                        Debug.LogWarning("Passaggio di stato non permesso da : " + _oldState + " a : " + _newState);
                    }
                    break;
                case FlowState.EndRound:
                    if (_oldState == FlowState.PreEndRound || _oldState == FlowState.Pause)
                    {
                        _currentState = _newState;
                        EndRoundActions();
                    }
                    else
                    {
                        Debug.LogWarning("Passaggio di stato non permesso da : " + _oldState + " a : " + _newState);
                    }
                    break;

                case FlowState.QuitGame:
                    if (_oldState == FlowState.MainMenu || _oldState == FlowState.Pause)
                    {
                        _currentState = _newState;
                        QuitGameActions();
                    }
                    else
                    {
                        Debug.LogWarning("Passaggio di stato non permesso da : " + _oldState + " a : " + _newState);
                    }
                    break;
                #endregion

                #region Test Flow
                case FlowState.InitTestScene:
                    if (_oldState == FlowState.MainMenu)
                    {
                        _currentState = _newState;
                        InitTestSceneActions();
                    }
                    else
                    {
                        Debug.LogWarning("Passaggio di stato non permesso da : " + _oldState + " a : " + _newState);
                    }
                    break;

                case FlowState.TestGameplay:
                    if (_oldState == FlowState.InitTestScene)
                    {
                        _currentState = _newState;
                        TestGameplayActions();
                    }
                    else
                    {
                        Debug.LogWarning("Passaggio di stato non permesso da : " + _oldState + " a : " + _newState);
                    }
                    break;
                case FlowState.ExitTestScene:
                    if (_oldState == FlowState.TestGameplay)
                    {
                        _currentState = _newState;
                        ExitTestSceneActions();
                    }
                    else
                    {
                        Debug.LogWarning("Passaggio di stato non permesso da : " + _oldState + " a : " + _newState);
                    }
                    break;
                    #endregion
            }
        }

        #region Game Flow Actions
        void SetupGameActions()
        {
            GameManager.I.UIMng = GameObject.Instantiate(GameManager.I.UIManagerPrefab, GameManager.I.transform).GetComponentInChildren<UIManager>();

            GameManager.I.PoolMng = GameManager.I.GetComponentInChildren<PoolManager>();
            GameManager.I.PoolMng.Init();

            GameManager.I.EnemyMng = GameManager.I.GetComponentInChildren<EnemyManager>();
            GameManager.I.AmmoController = GameManager.I.GetComponent<AmmoCratesController>();

            GameManager.I.LevelMng = GameManager.I.GetComponent<LevelManager>();

            GameManager.I.Player = GameManager.I.GetComponent<Player>();
            if (GameManager.I.Player != null)
                GameManager.I.Player.Init();

            GameManager.I.AudioMng = GameManager.I.GetComponentInChildren<AudioManager>();
            GameManager.I.AudioMng.Init();

            GameManager.I.CursorCtrl = GameManager.I.GetComponentInChildren<CursorController>();
            GameManager.I.PPCtrl = Camera.main.GetComponentInChildren<PostProcessController>();

            GameManager.I.ComboCounter = GameManager.I.GetComponent<ComboCounter>();           
            GameManager.I.ScoreCounter = GameManager.I.GetComponentInChildren<ScoreCounter>();

            CurrentState = FlowState.MainMenu;
        }

        void MainMenuActions()
        {
            GameManager.I.CursorCtrl.SetCursor(false);
            GameManager.I.LevelMng.MapIndex = 0;
            GameManager.I.UIMng.MainMenuActions();
            GameManager.I.AudioMng.PlaySound(Clips.MenuMusic);
        }

        void ManageMapActions()
        {
            GameManager.I.UIMng.LoadingActions();

            if (GameManager.I.LevelMng.EndingStaus == LevelEndingStaus.Lost)
                GameManager.I.ScoreCounter.ResetCounter();

            GameManager.I.LevelMng.ReInit();
            GameManager.I.LevelMng.MapIndex++;
        }

        void InitGameplayElementsActions()
        {
            if(GameManager.I.LevelMng.Level > 0)
                GameManager.I.LevelMng.Init(GameManager.I.KillsToWinPerLevel[GameManager.I.LevelMng.Level - 1]);

            if (GameManager.I.Player != null)
                GameManager.I.Player.InitCharacter();

            GameManager.I.EnemyMng.Init();
            GameManager.I.UIMng.GameplayActions();
            GameManager.I.AmmoController.Init();

            GameManager.I.CursorCtrl.SetCursor(true);

            if (GameManager.I.LevelMng.MapIndex <= 4)
                GameManager.I.PPCtrl.SetPostProcess(PostProcessController.MapType.Forest);
            else if (GameManager.I.LevelMng.MapIndex <= 8)
                GameManager.I.PPCtrl.SetPostProcess(PostProcessController.MapType.Mine);
            else if (GameManager.I.LevelMng.MapIndex <= 12)
                GameManager.I.PPCtrl.SetPostProcess(PostProcessController.MapType.City);

            GameManager.I.ComboCounter.Init(GameManager.I.ComboCounterTimer);
            GameManager.I.ScoreCounter.Init();

            CurrentState = FlowState.Gameplay;
        }

        void GameplayActions()
        {
            GameManager.I.AudioMng.PlaySound(Clips.GameplayMusic);

            if (GameManager.I.LevelMng.MapIndex <= 4)
                GameManager.I.AudioMng.PlaySound(Clips.ForestAmbience);
            else if (GameManager.I.LevelMng.MapIndex <= 8)
                GameManager.I.AudioMng.PlaySound(Clips.MineAmbience);
            else if (GameManager.I.LevelMng.MapIndex <= 12)
                GameManager.I.AudioMng.PlaySound(Clips.CityAmbience);
        }

        void PauseActions(bool _isGamePaused)
        {
            GameManager.I.EnemyMng.CanSpawn = !_isGamePaused;
            GameManager.I.EnemyMng.ToggleAllAIs(!_isGamePaused);
            GameManager.I.AudioMng.TogglePauseAll(_isGamePaused, false);
            GameManager.I.CursorCtrl.SetCursor(!_isGamePaused);

            if (_isGamePaused)
            {
                Time.timeScale = 0;
                GameManager.I.UIMng.PauseActions();
            }
            else
            {
                Time.timeScale = 1;
                GameManager.I.UIMng.GameplayActions();
            }
        }

        void PreRoundEndActions()
        {
       
            GameManager.I.EnemyMng.EndGameplayActions();

            if (GameManager.I.LevelMng.EndingStaus == LevelEndingStaus.Lost)
            {
                GameManager.I.Player.DeadCharacter();
                GameManager.I.AudioMng.PlaySound(Clips.GameLost);
            }
            else if (GameManager.I.LevelMng.EndingStaus == LevelEndingStaus.Won)
                GameManager.I.AudioMng.PlaySound(Clips.GameWon);

            GameManager.I.CursorCtrl.SetCursor(false);
            GameManager.I.LevelMng.ClearCombos();
            GameManager.I.Player.ResetAnimations();

            GameManager.I.StartEndPreRoundCoroutine();
        }

        void EndRoundActions()
        {
            Time.timeScale = 1;
            GameManager.I.EnemyMng.ToggleAllAIs(true);
            GameManager.I.AmmoController.DeleteAllAmmoCrate();
            GameManager.I.AudioMng.StopAllSound();

            GameManager.I.ScoreCounter.Clear();
            GameManager.I.ComboCounter.Clear();
            GameManager.I.ScoreCounter.EndRoundAction(GameManager.I.LevelMng.EndingStaus);

            if (GameManager.I.LevelMng.EndingStaus == LevelEndingStaus.Interrupted)
            {
                GameManager.I.UIMng.LoadingActions();
                GameManager.I.LevelMng.MapIndex = 0;
                return;
            }

            GameManager.I.Player.Character.ReInit();
            GameManager.I.UIMng.GameOverActions(GameManager.I.LevelMng.EndingStaus);
        }

        void QuitGameActions()
        {
            Application.Quit();
        }
        #endregion

        #region Test Flow Actions
        void InitTestSceneActions()
        {
            GameManager.I.UIMng.EnableValuesPanel(GameManager.I.Player.CharacterData, GameManager.I.EnemyMng.Data.EnemiesData[0]); 
        }

        void TestGameplayActions()
        {
            GameManager.I.CursorCtrl.SetCursor(true);

            if (GameManager.I.Player != null)
            {
                GameManager.I.Player.InitCharacter(true);
                CharacterData newData = GameObject.Instantiate(GameManager.I.Player.CharacterData);
                newData.Life = GameManager.I.UIMng.UI_ValuesPanelCtrl.CharacterDataForTestScene.Life;
                newData.MovementSpeed = GameManager.I.UIMng.UI_ValuesPanelCtrl.CharacterDataForTestScene.MovementSpeed;
                newData.RotationSpeed = GameManager.I.UIMng.UI_ValuesPanelCtrl.CharacterDataForTestScene.RotationSpeed;
                GameManager.I.Player.Character.Init(GameManager.I.Player, newData, true);
            }

            GameManager.I.EnemyMng.IgnoreTarget = !GameManager.I.UIMng.UI_ValuesPanelCtrl.FollowPlayerToggle.isOn;
            GameManager.I.EnemyMng.Init(true);

            foreach (EnemyGenericData enemyData in GameManager.I.EnemyMng.DataInstance.EnemiesData)
            {
                enemyData.Life = GameManager.I.UIMng.UI_ValuesPanelCtrl.EnemyGenericDataForTestScene.Life;
                enemyData.MeleeDamage = GameManager.I.UIMng.UI_ValuesPanelCtrl.EnemyGenericDataForTestScene.MeleeDamage;
                enemyData.Speed = GameManager.I.UIMng.UI_ValuesPanelCtrl.EnemyGenericDataForTestScene.Speed;
                enemyData.MeleeDamageRange = GameManager.I.UIMng.UI_ValuesPanelCtrl.EnemyGenericDataForTestScene.MeleeDamageRange;
            }

            GameManager.I.EnemyMng.SpawnEnemyForTestScene();
            GameManager.I.UIMng.GameplayActions();

            GameManager.I.AudioMng.PlaySound(Clips.GameplayMusic);

            if (GameManager.I.LevelMng.MapIndex <= 4)
                GameManager.I.AudioMng.PlaySound(Clips.ForestAmbience);
            else if (GameManager.I.LevelMng.MapIndex <= 8)
                GameManager.I.AudioMng.PlaySound(Clips.MineAmbience);
            else if (GameManager.I.LevelMng.MapIndex <= 12)
                GameManager.I.AudioMng.PlaySound(Clips.CityAmbience);
        }

        void ExitTestSceneActions()
        {
            GameManager.I.CursorCtrl.SetCursor(false);
            GameManager.I.EnemyMng.EndGameplayActions();
            GameManager.I.LevelMng.MapIndex = 0;
        }
        #endregion
    }

    public enum FlowState
    {
        None = 0,
        SetupGame,
        MainMenu,
        ManageMap,
        InitGameplayElements,
        Gameplay,
        Pause,
        PreEndRound,
        EndRound,
        QuitGame,

        InitTestScene,
        TestGameplay,
        ExitTestScene
    }
}