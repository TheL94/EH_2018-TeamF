using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Player : MonoBehaviour
    {
        void FixedUpdate()
        {
            CheckMovementInput();
        }

        private void Update()
        {
            CheckInput();
        }

        public void Init()
        {
            Character = FindObjectOfType<Character>();
        }

        #region Character
        public Character Character { get; private set; }
        public CharacterData CharacterData;

        /// <summary>
        /// Chiama l'init per il character
        /// </summary>
        /// <param name="_isTestScene">Se true, il character deve essere inizializzato per la scena di test, altrimenti per una scena di gioco normale</param>
        public void InitCharacter(bool _isTestScene = false)
        {
            Vector3 playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform.position;
            Character.transform.position = playerSpawn;

            if(!_isTestScene)
                Character.Init(this, Instantiate(CharacterData), _isTestScene);
        }

        public void CharacterDeath()
        {
            GameManager.I.AudioMng.PlaySound(Clips.CharacterDeath);
            GameManager.I.LevelMng.CheckGameStatus();
        }
        #endregion

        #region Input

        void CheckInput()
        {
            if (Character.IsParalyzed)
                return;

            if (GameManager.I.CurrentState == FlowState.Gameplay || GameManager.I.CurrentState == FlowState.TestGameplay)
            {
                if (Character.Life <= 0)
                    return;

                if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown(KeyCode.E))
                {
                    Character.SelectPreviousAmmo();
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyDown(KeyCode.Q))
                {
                    Character.SelectNextAmmo();
                }

                if (Input.GetMouseButton(1))
                    Character.DefaultShot();

                if (Input.GetMouseButton(0))
                    Character.ElementalShot();
            }
            if (GameManager.I.CurrentState == FlowState.MainMenu || GameManager.I.CurrentState == FlowState.Pause || GameManager.I.CurrentState == FlowState.EndRound)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    GameManager.I.UIMng.CurrentMenu.GoUpInMenu();
                if (Input.GetKeyDown(KeyCode.DownArrow))
                    GameManager.I.UIMng.CurrentMenu.GoDownInMenu();
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                    GameManager.I.UIMng.CurrentMenu.Select();
            }
        }

        void CheckMovementInput()
        {
            if (Character.IsParalyzed)
                return;

            if (GameManager.I.CurrentState == FlowState.Gameplay || GameManager.I.CurrentState == FlowState.TestGameplay)
            {
                if (Character.Life <= 0)
                    return;

                Vector3 finalDirection = new Vector3();

                if (Input.GetKey(KeyCode.W))
                    finalDirection += transform.forward;

                if (Input.GetKey(KeyCode.S))
                    finalDirection += -transform.forward;
                if (Input.GetKey(KeyCode.A))
                    finalDirection += -transform.right;

                if (Input.GetKey(KeyCode.D))
                    finalDirection += transform.right;

                if (Input.GetKeyDown(KeyCode.Space))
                    Character.movement.Dash(finalDirection);

                Character.movement.Move(finalDirection);

                Character.movement.Turn();
            }
        }
        #endregion
    }
}