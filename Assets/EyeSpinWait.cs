using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSpinWait : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetFloat("AngleSum", 0.0f);
        animator.SetInteger("AnimationType", -1);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ///int t = animator.GetInteger("AnimationType");
        ///if (t == 2 || t == 3) animator.SetInteger("AnimationType", -1);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetInteger("SpinRoute", (int)Random.Range(1.0f,6.0f)-1);
        /*
        animator.SetInteger("SpinRoute", animator.GetInteger("SpinRoute") + 1);
        if (animator.GetInteger("SpinRoute") == 6) animator.SetInteger("SpinRoute", 0);
        animator.SetInteger("AnimationType", 3);
        */
    }

}