using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(FlowManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager I;

    public FlowState CurrentState { get { return flowMng.CurrentState; } }

    FlowManager flowMng;

    public EnemySpawner EnemySpn;
    public GameObject PlayerPrefab;

    [HideInInspector]
    public UIManager UIMng;

    public GameObject UIManagerPrefab;

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
        UIMng = Instantiate(UIManagerPrefab, transform).GetComponentInChildren<UIManager>();
        ChangeFlowState(FlowState.Loading);
    }

    void ClearScene()
    {
        //EnemySpn.DeleteAllEnemy();
    }

    #region API

    public void Init()
    {
        Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
    }

    #region Game Flow
    /// <summary>
    /// Funzione che innesca il cambio di stato
    /// </summary>
    public void ChangeFlowState(FlowState _stateToSet)
    {
        flowMng.ChageState(_stateToSet);
    }

    public void LoadingActions()
    {
        ChangeFlowState(FlowState.Menu);
    }

    public void MenuActions()
    {
        UIMng.MainMenuActions();
    }

    public void GameplayActions()
    {
        UIMng.GameplayActions();
    }

    public void EndGameActions()
    {
        UIMng.GameOverActions();
        ClearScene();
    }

    public void CloseApplicationActions()
    {
        Application.Quit();
    }
    #endregion
    #endregion
}