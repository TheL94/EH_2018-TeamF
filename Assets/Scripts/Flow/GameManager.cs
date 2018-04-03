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
        [HideInInspector]
        public EnemyManager EnemyMng;
        [HideInInspector]
        public AmmoCratesController AmmoController;

        [HideInInspector]
        public UIManager UIMng;
        public GameObject UIManagerPrefab;
        [HideInInspector]
        public Player Player;
  
        public float KillsToWin;
        public float ComboCounterTimer;

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
            ChangeFlowState(FlowState.InitGame);
            // NELLO START NON CI INFILARE NIENTE ! USA L'AZIONE DI LOADING
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ChangeFlowState(FlowState.GameLost);
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

        public void InitGame()
        {
            UIMng = Instantiate(UIManagerPrefab, transform).GetComponentInChildren<UIManager>();

            GetComponent<ComboCounter>().Init(ComboCounterTimer);
            EnemyMng = GetComponent<EnemyManager>();
            AmmoController = GetComponent<AmmoCratesController>();

            LevelMng = new LevelManager(KillsToWin);

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
            if (Player != null)
                Player.InitCharacter();

            EnemyMng.Init(Player.Character);
            UIMng.GameplayActions();
            AmmoController.Init();
            ChangeFlowState(FlowState.Gameplay);
        }

        public void EnterTestSceneActions()
        {
            if (Player != null)
                Player.InitCharacter(true);

            EnemyMng.Init(Player.Character,EnemyMng.DataInstance, true);
            UIMng.GameplayActions();
            ChangeFlowState(FlowState.Gameplay);
        }

        public void PauseActions()
        {

        }

        public void GameWonActions()
        {
            UIMng.GameOverActions(true);
            LevelMng.UpdateLevel();
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
            UIMng.EnableValuesPanel(Player.CharacterData, EnemyMng.Data.EnemiesData[0]);               // Farsi restituire i dati dal data manager

            GameObject tempobj = Instantiate(Resources.Load("TestScenePrefab/EnemyManager_TS"), transform) as GameObject;
            EnemyMng = tempobj.GetComponent<EnemySpawner_TS>();
            EnemyMng.InitDataForTestScene();
        }

        #endregion
    }
}