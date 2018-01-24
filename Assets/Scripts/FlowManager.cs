using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour
{
    private FlowState _currentState = FlowState.None;
    public FlowState CurrentState
    {
        get { return _currentState; }
        private set
        {
            if(_currentState != value)
            {
                FlowState oldState = _currentState;
                _currentState = value;
                OnStateChange(_currentState ,oldState);
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
                if (_oldState == FlowState.Loading)
                    GameManager.I.MenuActions();
                break;
            case FlowState.Gameplay:
                if(_oldState == FlowState.Menu)
                    GameManager.I.GameplayActions();
                break;
            case FlowState.EndGame:
                if (_oldState == FlowState.Gameplay)
                    GameManager.I.EndGameActions();
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
    Gameplay,
    EndGame
}
