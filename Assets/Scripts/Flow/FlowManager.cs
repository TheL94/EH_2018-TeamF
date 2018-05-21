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
                    if (_oldState == FlowState.MainMenu || _oldState == FlowState.EndRound)
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

                case FlowState.EndRound:
                    if (_oldState == FlowState.Gameplay || _oldState == FlowState.Pause)
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

            GameManager.I.GetComponent<ComboCounter>().Init(GameManager.I.ComboCounterTimer);
            GameManager.I.EnemyMng = GameManager.I.GetComponent<EnemyManager>();
            GameManager.I.AmmoController = GameManager.I.GetComponent<AmmoCratesController>();

            GameManager.I.LevelMng = new LevelManager();

            GameManager.I.Player = GameManager.I.GetComponent<Player>();
            if (GameManager.I.Player != null)
                GameManager.I.Player.Init();

            GameManager.I.AudioMng = GameManager.I.GetComponentInChildren<AudioManager>();
            GameManager.I.AudioMng.Init();

            GameManager.I.CursorCtrl = GameManager.I.GetComponentInChildren<CursorController>();        

            CurrentState = FlowState.MainMenu;
        }

        void MainMenuActions()
        {
            GameManager.I.CursorCtrl.SetCursor(false);
            GameManager.I.LevelMng.Level = 0;
            GameManager.I.UIMng.MainMenuActions();
        }

        void ManageMapActions()
        {
            GameManager.I.LevelMng.ReInit();
            GameManager.I.LevelMng.Level++;
        }

        void InitGameplayElementsActions()
        {
            if (GameManager.I.Player != null)
                GameManager.I.Player.InitCharacter();

            GameManager.I.EnemyMng.Init();
            GameManager.I.UIMng.GameplayActions();
            GameManager.I.AmmoController.Init();

            GameManager.I.CursorCtrl.SetCursor(true);

            CurrentState = FlowState.Gameplay;
        }

        void PauseActions(bool _isGamePaused)
        {
            

            GameManager.I.EnemyMng.ToggleAllAIs(!_isGamePaused);
            GameManager.I.AudioMng.TogglePauseAll(_isGamePaused);

            if (_isGamePaused)
            {
                GameManager.I.CursorCtrl.SetCursor(false);
                GameManager.I.UIMng.PauseActions();
            }
            else
            {
                GameManager.I.CursorCtrl.SetCursor(true);
                GameManager.I.UIMng.GameplayActions();
            }
        }

        void EndRoundActions()
        {
            GameManager.I.CursorCtrl.SetCursor(false);
            GameManager.I.EnemyMng.EndGameplayActions();

            if (GameManager.I.LevelMng.EndingStaus == LevelEndingStaus.Interrupted)
            {
                GameManager.I.LevelMng.Level = 0;
                CurrentState = FlowState.MainMenu;
                return;
            }

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
        }

        void ExitTestSceneActions()
        {
            GameManager.I.CursorCtrl.SetCursor(false);
            GameManager.I.EnemyMng.EndGameplayActions();
            GameManager.I.LevelMng.Level = 0;
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
        EndRound,
        QuitGame,

        InitTestScene,
        TestGameplay,
        ExitTestScene
    }
}