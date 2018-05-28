using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.Pool;

namespace TeamF
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager I;

        public FlowState CurrentState { get { return flowMng.CurrentState; } set { flowMng.CurrentState = value; } }

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
        [HideInInspector]
        public PoolManager PoolMng;
        [HideInInspector]
        public AudioManager AudioMng;
        [HideInInspector]
        public CursorController CursorCtrl;
        [HideInInspector]
        public PostProcessController PPCtrl;

        public List<float> KillsToWinPerLevel = new List<float>();
        public float ComboCounterTimer;

        FlowManager flowMng;

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
            // NELLO START NON CI INFILARE NIENTE ! USA LO STATO DI SETUP GAME.
            flowMng = new FlowManager();
            CurrentState = FlowState.SetupGame;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (CurrentState == FlowState.TestGameplay)
                    CurrentState = FlowState.ExitTestScene;
                else            
                    CurrentState = FlowState.Pause;
            }
        }
    }
}