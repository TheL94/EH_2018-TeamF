using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TeamF {
    [RequireComponent(typeof(FlowManager))]
    public class GameManager : MonoBehaviour
    {
        public static GameManager I;

        public FlowState CurrentState { get { return flowMng.CurrentState; } }

        FlowManager flowMng;

        public EnemySpawner EnemySpn;
        public GameObject PlayerPrefab;
        public AmmoCratesController AmmoController;
        [HideInInspector]
        public UIManager UIMng;

        public GameObject UIManagerPrefab;

        bool _isWin;

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
            EnemySpn.EndGameActions();
        }

        #region API

        public void Init()
        {
            Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
        }

        public void VictoryActions()
        {
            _isWin = true;
            ChangeFlowState(FlowState.EndGame);
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
            _isWin = false;
            UIMng.GameplayActions();
            AmmoController.Init();
            EnemySpn.Init();
        }

        public void EndGameActions()
        {
            UIMng.GameOverActions(_isWin);
            ClearScene();
        }

        public void CloseApplicationActions()
        {
            Application.Quit();
        }
        #endregion
        #endregion
    }
}