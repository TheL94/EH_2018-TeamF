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
        public LevelManager LevelMng;
        public EnemyController EnemyCtrl;

        public GameObject PlayerPrefab;
        public AmmoCratesController AmmoController;

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
            ChangeFlowState(FlowState.Loading);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                CloseApplicationActions();
        }

        void ClearScene()
        {
            EnemyCtrl.EndGameplayActions();
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
            UIMng = Instantiate(UIManagerPrefab, transform).GetComponentInChildren<UIManager>();
            ChangeFlowState(FlowState.Menu);
        }

        public void MenuActions()
        {
            UIMng.MainMenuActions();
        }

        public void GameplayActions()
        {
            LevelMng = new LevelManager(this ,30f);

            UIMng.GameplayActions();
            AmmoController.Init();
            EnemyCtrl.Init(LevelMng);
        }

        public void EndGameActions()
        {
            UIMng.GameOverActions(LevelMng.IsGameWon);
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