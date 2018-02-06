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
            avatar.Init(this);
        }

        void Update()
        {
            CheckInput();
        }

        void CheckInput()
        {
            if (GameManager.I.CurrentState == FlowState.Gameplay)
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

                avatar.movement.Rotate();
            }
            if (GameManager.I.CurrentState != FlowState.Gameplay)
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