using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class SpaceSceneController : MonoBehaviour
    {
        public delegate void OnEndAnimation();
        public static OnEndAnimation EndSpanceAnimation;

        int endedAnimations;

        private void OnEnable()
        {
            EndSpanceAnimation += UpdateAnimationsCount;
        }

        void UpdateAnimationsCount()
        {
            endedAnimations++;
            if (endedAnimations == 2)
                GameManager.I.CurrentState = FlowState.ManageMap;
        }

        private void OnDisable()
        {
            EndSpanceAnimation -= UpdateAnimationsCount;
        }
    }
}