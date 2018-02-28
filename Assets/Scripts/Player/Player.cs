using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity_Framework.ControllerInput;

namespace TeamF
{
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// Se il player ha attraversato una nube paralizzante, viene settata questa variabile che blocca gli input.
        /// </summary>
        public bool IsParalized { get; set; }

        Character character;
        ControllerInput controllerInput;

        public void AvatarDeath()
        {
            GameManager.I.LevelMng.GoToGameLost();
        }

        void Update()
        {
            CheckInput();
        }

        public void Init()
        {
            controllerInput = new ControllerInput(0);
            character = GetComponent<Character>();
            IsParalized = false;
        }

        /// <summary>
        /// Chiama l'init per il character
        /// </summary>
        /// <param name="_isTestScene">Se true, il character deve essere inizializzato per la scena di test, altrimenti per una scena di gioco normale</param>
        public void CharacterInit(bool _isTestScene = false)
        {
            character.Init(this, _isTestScene);
        }

        void CheckInput()
        {
            InputStatus status = controllerInput.GetPlayerInputStatus();
            if (IsParalized)
                return;
            if (status.IsConnected)
                CheckControllerInput();
            else
                CheckKeyboardInput();
        }

        void CheckControllerInput()
        {

        }

        void CheckKeyboardInput()
        {
            if (GameManager.I.CurrentState == FlowState.Gameplay)
            {
                if (character.Life <= 0)
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
                    character.SelectPreviousAmmo();

                }
                else if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown(KeyCode.Q))
                {
                    character.SelectNextAmmo();
                }

                character.movement.Move(finalDirection.normalized);


                if (Input.GetMouseButtonDown(0))
                    character.Shot();
                if (Input.GetMouseButton(0))
                    character.FullAutoShot();

                character.movement.Rotate();
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
    }
}