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
        public EnemyManager EnemyMng;

        public AmmoCratesController AmmoController;

        [HideInInspector]
        public UIManager UIMng;
        public GameObject UIManagerPrefab;
        [HideInInspector]
        public Player Player;

        public float KillsToWin;

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
            // NELLO START NON CI INFILARE NIENTE ! USA L'AZIONE DI LOADING
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                CloseApplicationActions();
        }

        void ClearScene()
        {
            EnemyMng.EndGameplayActions();
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

            Player = GetComponent<Player>();
            if (Player != null)
                Player.Init();

            ChangeFlowState(FlowState.Menu);
        }

        public void MenuActions()
        {
            UIMng.MainMenuActions();
        }

        public void EnterGameplayActions()
        {
            LevelMng = new LevelManager(KillsToWin);

            if (Player != null)
                Player.InitCharacter();

            UIMng.GameplayActions();
            AmmoController.Init();
            EnemyMng.Init(Player.Character);
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

        /// <summary>
        /// Attiva il pannello dei valori e gli passa i dati per settare i campi all'inizio
        /// </summary>
        public void EnterValuesMenu()
        {
            UIMng.EnableValuesPanel(Player.Character.Data, EnemyMng.SpawnerData.EnemiesData[0]);               // Farsi restituire i dati dal data manager
        }

        //TODO: Operazioni da svolgere dopo aver settato i valori del pannello dei valori
        public void EnterTestScene()
        {
            if (Player != null)
                Player.InitCharacter(true);

            UIMng.GameplayActions();
            EnemyMng.Init(null, true);
            ChangeFlowState(FlowState.Gameplay);
        }

        #endregion
    }
}