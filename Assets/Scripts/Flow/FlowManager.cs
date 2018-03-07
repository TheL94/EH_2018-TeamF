using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class FlowManager
    {
        private FlowState _currentState = FlowState.None;
        public FlowState CurrentState
        {
            get { return _currentState; }
            private set
            {
                if (_currentState != value)
                {
                    FlowState oldState = _currentState;
                    _currentState = value;
                    OnStateChange(_currentState, oldState);
                }
            }
        }

        private void OnStateChange(FlowState _newState, FlowState _oldState)
        {
            switch (_newState)
            {
                case FlowState.Loading:
                    GameManager.I.LoadingActions();
                    break;
                case FlowState.Menu:
                    if (_oldState == FlowState.Loading || _oldState == FlowState.ExitGameplay)
                        GameManager.I.MenuActions();
                    break;
                case FlowState.EnterGameplay:
                    if (_oldState == FlowState.Menu)
                        GameManager.I.EnterGameplayActions();
                    break;
                case FlowState.EnterTestScene:
                    if (_oldState == FlowState.Menu)
                        GameManager.I.EnterTestSceneActions();
                    break;
                case FlowState.Gameplay:
                    break;
                case FlowState.Pause:
                    GameManager.I.PauseActions();
                    break;
                case FlowState.GameWon:
                    GameManager.I.GameWonActions();
                    break;
                case FlowState.GameLost:
                    GameManager.I.GameLostActions();
                    break;
                case FlowState.ExitGameplay:
                    if (_oldState == FlowState.GameWon || _oldState == FlowState.GameLost)
                        GameManager.I.ExitGameplayActions();
                    break;
            }
        }

        public void ChageState(FlowState _stateToSet)
        {
            CurrentState = _stateToSet;
        }
    }

    public enum FlowState
    {
        None,
        Loading,
        Menu,
        EnterGameplay,
        EnterTestScene,
        Gameplay,
        Pause,
        GameWon,
        GameLost,
        ExitGameplay,
    }
}