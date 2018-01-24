﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(FlowManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager I;

    public FlowState CurrentState { get { return flowMng.CurrentState; } }

    FlowManager flowMng;

    public UI_GameplayController UI_gameplayeCrtl;
        
    void Awake()
    {
        //Singleton paradigm
        if (I == null)
            I = this;
        else
            DestroyImmediate(gameObject);
    }

    void Start()
    {
        flowMng = GetComponent<FlowManager>();
        ChageFlowState(FlowState.Loading);
    }

    #region API

    #region Game Flow
    /// <summary>
    /// Funzione che innesca il cambio di stato
    /// </summary>
    public void ChageFlowState(FlowState _stateToSet)
    {
        flowMng.ChageState(_stateToSet);
    }

    public void LoadingActions()
    {
        ChageFlowState(FlowState.Menu);
    }

    public void MenuActions()
    {

    }

    public void GameplayActions()
    {

    }

    public void EndGameActions()
    {
        ChageFlowState(FlowState.Menu);
    }

    public void CloseApplicationActions()
    {
        Application.Quit();
    }
    #endregion
    #endregion
}