using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity_Framework.ControllerInput;

namespace TeamF
{
    public class Player : MonoBehaviour
    {
        void Update()
        {
            CheckInput();
        }

        public void Init()
        {
            Character = FindObjectOfType<Character>();
            controllerInput = new ControllerInput(0);
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
            Character.Init(this, Instantiate(CharacterData), _isTestScene);
        }

        public void CharacterDeath()
        {
            GameManager.I.LevelMng.GoToGameLost();
        }
        #endregion

        #region Input
        ControllerInput controllerInput;

        void CheckInput()
        {
            //InputStatus status = controllerInput.GetPlayerInputStatus();
            if (Character.IsParalized)
                return;
            else
                CheckKeyboardInput();
        }

        void CheckKeyboardInput()
        {
            if (GameManager.I.CurrentState == FlowState.Gameplay)
            {
                if (Character.Life <= 0)
                    return;

                Vector3 finalDirection = new Vector3();

                if (Input.GetKey(KeyCode.W))
                    finalDirection += transform.right;
                if (Input.GetKey(KeyCode.S))
                    finalDirection += -transform.right;
                if (Input.GetKey(KeyCode.A))
                    finalDirection += transform.forward;
                if (Input.GetKey(KeyCode.D))
                    finalDirection += -transform.forward;

                if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyDown(KeyCode.E))
                {
                    Character.SelectPreviousAmmo();

                }
                else if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown(KeyCode.Q))
                {
                    Character.SelectNextAmmo();
                }

                Character.movement.Move(finalDirection.normalized);


                if (Input.GetMouseButtonDown(0))
                    Character.Shot();
                if (Input.GetMouseButton(0))
                    Character.FullAutoShot();

                Character.movement.Rotate();
            }
            if (GameManager.I.CurrentState != FlowState.EnterGameplay && GameManager.I.CurrentState != FlowState.Gameplay)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    GameManager.I.UIMng.CurrentMenu.GoUpInMenu();
                if (Input.GetKeyDown(KeyCode.DownArrow))
                    GameManager.I.UIMng.CurrentMenu.GoDownInMenu();
                if (Input.GetKeyDown(KeyCode.Space))
                    GameManager.I.UIMng.CurrentMenu.Select();
            }
        }
        #endregion
    }
}