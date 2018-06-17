using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamF;

public class EndSpaceAnim : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (SpaceSceneController.EndSpanceAnimation != null)
            SpaceSceneController.EndSpanceAnimation(); 
    }

}
