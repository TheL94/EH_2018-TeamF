using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Player : MonoBehaviour
    {
        Character character;

        public void AvatarDeath()
        {
            GameManager.I.LevelMng.GoToGameLost();
        }

        void Start()
        {
            character = GetComponent<Character>();
            character.Init(this);
        }

        void Update()
        {
            CheckInput();
        }

        void CheckInput()
        {
            if (GameManager.I.CurrentState == FlowState.Gameplay)
            {
                if (character.Life <= 0)
                    return;

                if (Input.GetKey(KeyCode.W))
                    character.movement.Move(transform.right);
                if (Input.GetKey(KeyCode.S))
                    character.movement.Move(-transform.right);
                if (Input.GetKey(KeyCode.A))
                    character.movement.Move(transform.forward);
                if (Input.GetKey(KeyCode.D))
                    character.movement.Move(-transform.forward);
                if (Input.GetMouseButtonDown(0))
                    character.Shot();
                if (Input.GetMouseButton(0))
                    character.FullAutoShot();

                if (Input.GetKeyDown(KeyCode.Alpha1)) {
                    character.SetActiveAmmo(ElementalType.Fire);
                }

                if (Input.GetKeyDown(KeyCode.Alpha2)) {
                    character.SetActiveAmmo(ElementalType.Water);
                }

                if (Input.GetKeyDown(KeyCode.Alpha3)) {
                    character.SetActiveAmmo(ElementalType.Poison);
                }

                if (Input.GetKeyDown(KeyCode.Alpha4)) {
                    character.SetActiveAmmo(ElementalType.Thunder);
                }
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    character.SetActiveAmmo(ElementalType.None);
                }
                character.movement.Rotate();
            }
            if (GameManager.I.CurrentState != FlowState.EnterGameplay)
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