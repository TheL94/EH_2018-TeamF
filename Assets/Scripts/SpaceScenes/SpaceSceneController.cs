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
        }

        private void Update()
        {
            if (GameManager.I.UIMng.UI_GameplayCtrl.gameObject.activeInHierarchy)
                GameManager.I.UIMng.UI_GameplayCtrl.gameObject.SetActive(false);
        }

        private void Start()
        {
            GameManager.I.UIMng.UI_GameplayCtrl.gameObject.SetActive(false);
            GameManager.I.IsPlayingCutScene = true;
            Cursor.visible = false;
            characterStartPosition = GameManager.I.Player.Character.transform.position;
            GameManager.I.Player.Character.transform.position = new Vector3(0, 100, 0);
        }

        void UpdateAnimationsCount()
        {
            endedAnimations++;

            if (endedAnimations == 2)
            {
                GameManager.I.IsPlayingCutScene = false;
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