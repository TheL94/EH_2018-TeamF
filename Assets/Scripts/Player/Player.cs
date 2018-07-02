using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Player : MonoBehaviour
    {
        bool controllerConnected;

        void FixedUpdate()
        {
            if (!GameManager.I.IsPlayingCutScene)
                CheckMovementInput();
        }

        private void Update()
        {
            if (!GameManager.I.IsPlayingCutScene)
            {
                CheckInput();
                CheckPause();
            }
        }

        public void Init()
        {
            Character = FindObjectOfType<Character>();
            string[] joyName = Input.GetJoystickNames();
            if (joyName.Length > 0)
                if (joyName[0] != string.Empty)
                    controllerConnected = true;
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
            GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
            if (spawnPoint == null)
                return;

            Vector3 playerSpawn = spawnPoint.transform.position;
            Character.transform.position = playerSpawn;

            if (!_isTestScene)
                Character.Init(this, Instantiate(CharacterData), _isTestScene);
        }

        public void CharacterDeath()
        {
            GameManager.I.AudioMng.PlaySound(Clips.CharacterDeath);
            GameManager.I.LevelMng.CheckGameStatus();
        }

        public void ResetAnimations()
        {
            Character.StopWalkAnimation();
        }

        public void DeadCharacter()
        {
            Character.GetComponentInChildren<Animator>().SetTrigger("IsDead");
        }
        #endregion

        #region Input
        bool canPressVertical = true;
        bool canPressHorizontal = true;

        void CheckInput()
        {
            if (Character.IsParalyzed)
                return;

            if (GameManager.I.CurrentState == FlowState.Gameplay || GameManager.I.CurrentState == FlowState.TestGameplay)
            {
                if (Character.Life <= 0)
                    return;

                #region Joystick
                if (controllerConnected)
                {
                    if (Input.GetButtonDown("A_Button"))
                        Character.SelectSpecificAmmo(2);

                    if (Input.GetButtonDown("B_Button"))
                        Character.SelectSpecificAmmo(1);

                    if (Input.GetButtonDown("X_Button"))
                        Character.SelectSpecificAmmo(3);

                    if (Input.GetButtonDown("Y_Button"))
                        Character.SelectSpecificAmmo(4);

                    if (Input.GetAxisRaw("Triggers") == 1)
                        Character.ElementalShot();

                    if (Input.GetAxisRaw("Triggers") == -1)
                        Character.DefaultShot();
                }
                #endregion

                #region KeyBoard
                else
                {
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
                #endregion
            }
            if (GameManager.I.CurrentState == FlowState.MainMenu || GameManager.I.CurrentState == FlowState.Pause || GameManager.I.CurrentState == FlowState.EndRound)
            {
                #region Joystick
                #region Vertical Input
                if (canPressVertical)
                {
                    if (Input.GetAxisRaw("Vertical") >= .8f)
                    {
                        canPressVertical = false;
                        GameManager.I.UIMng.CurrentMenu.GoUpInMenu();
                    }

                    if (Input.GetAxisRaw("Vertical") <= -.8f)
                    {
                        canPressVertical = false;
                        GameManager.I.UIMng.CurrentMenu.GoDownInMenu();
                    }
                }
                else
                {
                    if (controllerConnected)
                    {
                        if (Input.GetAxisRaw("Vertical") <= .5f && Input.GetAxisRaw("Vertical") >= -.5f)
                            canPressVertical = true;
                    }
                    else
                    {
                        if (Input.GetAxisRaw("Vertical") == 0)
                            canPressVertical = true;
                    }
                }
                #endregion

                #region Horizontal Input
                if (canPressHorizontal)
                {
                    if (controllerConnected)
                    {
                        if (Input.GetAxisRaw("Horizontal") <= -.8f)
                        {
                            canPressHorizontal = false;
                            GameManager.I.UIMng.CurrentMenu.GoLeftInMenu();
                        }

                        if (Input.GetAxisRaw("Horizontal") >= .8f)
                        {
                            canPressHorizontal = false;
                            GameManager.I.UIMng.CurrentMenu.GoRightInMenu();
                        }
                    }
                    else
                    {
                        if (Input.GetAxisRaw("Horizontal") == -1f)
                        {
                            canPressHorizontal = false;
                            GameManager.I.UIMng.CurrentMenu.GoLeftInMenu();
                        }

                        if (Input.GetAxisRaw("Horizontal") == 1f)
                        {
                            canPressHorizontal = false;
                            GameManager.I.UIMng.CurrentMenu.GoRightInMenu();
                        }
                    }
                }
                else
                {
                    if (controllerConnected)
                    {
                        if (Input.GetAxisRaw("Horizontal") <= .5f && Input.GetAxisRaw("Horizontal") >= -.5f)
                            canPressHorizontal = true;
                    }
                    else
                    {
                        if (Input.GetAxisRaw("Horizontal") == 0)
                            canPressHorizontal = true;
                    }
                }
                #endregion

                if (Input.GetButtonDown("B_Button"))
                    GameManager.I.UIMng.CurrentMenu.GoBack();
                #endregion

                #region KeyBoard
                //if (canPress)
                //{
                //    if (Input.GetAxisRaw("Vertical") == 1)
                //    {
                //        canPress = false;
                //        GameManager.I.UIMng.CurrentMenu.GoUpInMenu();
                //    }
                //    if (Input.GetAxisRaw("Vertical") == -1)
                //    {
                //        canPress = false;
                //        GameManager.I.UIMng.CurrentMenu.GoDownInMenu(); 
                //    }
                //}
                //else
                //{
                //    if (Input.GetAxisRaw("Vertical") == 0)
                //        canPress = true;
                //}

                #endregion

                if (Input.GetButtonDown("Submit"))
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

                #region Joystick
                if (controllerConnected)
                {
                    if (Input.GetAxis("Horizontal") > -.3f)
                        finalDirection += transform.right * Input.GetAxis("Horizontal");

                    if (Input.GetAxis("Horizontal") < .3f)
                        finalDirection += transform.right * Input.GetAxis("Horizontal");

                    if (Input.GetAxis("Vertical") > .3f)
                        finalDirection += transform.forward * Input.GetAxis("Vertical");

                    if (Input.GetAxis("Vertical") < -.3f)
                        finalDirection += transform.forward * Input.GetAxis("Vertical");

                }
                #endregion

                #region KeyBoard
                else
                {
                    if (Input.GetAxisRaw("Vertical") == -1f)
                        finalDirection += -transform.forward;

                    if (Input.GetAxisRaw("Vertical") == 1f)
                        finalDirection += transform.forward;

                    if (Input.GetAxisRaw("Horizontal") == -1f)
                        finalDirection += -transform.right;

                    if (Input.GetAxisRaw("Horizontal") == 1f)
                        finalDirection += transform.right;
                }
                #endregion

                if (Input.GetButtonDown("Dash"))
                    Character.movement.Dash(finalDirection);

                Character.movement.Move(finalDirection);

                Character.movement.Turn(GetRotation());
            }
        }

        void CheckPause()
        {
            if (Input.GetButtonDown("Pause"))
            {
                if (GameManager.I.CurrentState == FlowState.TestGameplay)
                    GameManager.I.CurrentState = FlowState.ExitTestScene;
                else if (GameManager.I.CurrentState == FlowState.Gameplay)
                    GameManager.I.CurrentState = FlowState.Pause;
                else if (GameManager.I.CurrentState == FlowState.Pause)
                    GameManager.I.CurrentState = FlowState.Gameplay;
                else if (GameManager.I.CurrentState == FlowState.MainMenu)
                    GameManager.I.UIMng.CurrentMenu.GoBack();
            }
        }

        Quaternion? GetRotation()
        {
            Quaternion? QuaterniontToReturn = null;
            if (controllerConnected)
            {
                Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * Input.GetAxisRaw("RVertical");
                if (playerDirection.sqrMagnitude > .0f)
                {
                    QuaterniontToReturn = Quaternion.LookRotation(playerDirection, transform.up);
                }
            }
            else
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit floorHit;

                if (Physics.Raycast(mouseRay, out floorHit, 100f, 1 << LayerMask.NameToLayer("MouseRaycast")))
                {
                    Vector3 playerToMouse = floorHit.point - Character.transform.position;
                    playerToMouse.y = 0;
                    QuaterniontToReturn = Quaternion.LookRotation(playerToMouse, Character.transform.up);
                }
            }
            return QuaterniontToReturn;
        }
        #endregion
    }
}