using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
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
            flowMng = new FlowManager();
            ChangeFlowState(FlowState.Loading);
            Player player = FindObjectOfType<Player>();
            if (player != null)
                player.Init();
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

        public void EnterGameplayActions()
        {
            LevelMng = new LevelManager(50f);

            Player player = FindObjectOfType<Player>();
            if (player != null)
                player.CharacterInit();

            UIMng.GameplayActions();
            AmmoController.Init();
            EnemyCtrl.Init();
            ChangeFlowState(FlowState.Gameplay);
        }

        public void EnterTestScene()
        {
            Player player = FindObjectOfType<Player>();
            if (player != null)
                player.CharacterInit(true);

            UIMng.GameplayActions();
            EnemyCtrl.Init(true);
            ChangeFlowState(FlowState.Gameplay);
        }

        public void PauseActions()
        {

        }

        public void GameWonActions()
        {
            UIMng.GameOverActions(true);
            ChangeFlowState(FlowState.ExitGameplay);
        }

        public void GameLostActions()
        {
            UIMng.GameOverActions(false);
            ChangeFlowState(FlowState.ExitGameplay);
        }

        public void ExitGameplayActions()
        {
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