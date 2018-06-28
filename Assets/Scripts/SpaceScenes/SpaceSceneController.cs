using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class SpaceSceneController : MonoBehaviour
    {
        public delegate void OnEndAnimation();
        public static OnEndAnimation EndSpanceAnimation;

        Vector3 characterStartPosition;
        int endedAnimations;

        private void OnEnable()
        {
            EndSpanceAnimation += UpdateAnimationsCount;
            GameManager.I.IsPlayingSequnce = true;
            characterStartPosition = GameManager.I.Player.Character.transform.position;
            GameManager.I.Player.Character.transform.position = new Vector3(0, 100, 0);
            Cursor.visible = false;
        }

        private void Start()
        {
            GameManager.I.UIMng.UI_GameplayCtrl.gameObject.SetActive(false);
        }

        void UpdateAnimationsCount()
        {
            endedAnimations++;

            if (endedAnimations == 2)
            {
                GameManager.I.UIMng.UI_GameplayCtrl.gameObject.SetActive(true);
                GameManager.I.IsPlayingSequnce = false;
                GameManager.I.Player.Character.transform.position = characterStartPosition;
                Cursor.visible = true;
                GameManager.I.CurrentState = FlowState.ManageMap;
            }
        }

        private void OnDisable()
        {
            EndSpanceAnimation -= UpdateAnimationsCount;
        }
    }
}