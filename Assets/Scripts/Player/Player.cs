using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class Player : MonoBehaviour
    {
        AvatarController avatar;

        void Start()
        {
            avatar = GetComponent<AvatarController>();
            avatar.Init(this, GameManager.I.LevelMng);
        }

        void Update()
        {
            CheckInput();
        }

        void CheckInput()
        {
            if (GameManager.I.CurrentState == FlowState.EnterGameplay)
            {
                if (avatar.Life <= 0)
                    return;

                if (Input.GetKey(KeyCode.W))
                    avatar.movement.Move(transform.forward);
                if (Input.GetKey(KeyCode.S))
                    avatar.movement.Move(-transform.forward);
                if (Input.GetKey(KeyCode.A))
                    avatar.movement.Move(-transform.right);
                if (Input.GetKey(KeyCode.D))
                    avatar.movement.Move(transform.right);
                if (Input.GetMouseButtonDown(0))
                    avatar.Shot();
                if (Input.GetMouseButton(0))
                    avatar.FullAutoShot();

                if (Input.GetKeyDown(KeyCode.Alpha1)) {
                    avatar.SetActiveAmmo(ElementalType.Fire);
                }

                if (Input.GetKeyDown(KeyCode.Alpha2)) {
                    avatar.SetActiveAmmo(ElementalType.Water);
                }

                if (Input.GetKeyDown(KeyCode.Alpha3)) {
                    avatar.SetActiveAmmo(ElementalType.Poison);
                }

                if (Input.GetKeyDown(KeyCode.Alpha4)) {
                    avatar.SetActiveAmmo(ElementalType.Thunder);
                }
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    avatar.SetActiveAmmo(ElementalType.None);
                }
                avatar.movement.Rotate();
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