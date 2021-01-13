using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSpinning : StateMachineBehaviour {
    public AnimationClip spin, spinR1, spinR2, spinRA, spinRS;
    float[] velocity = new float[5];
    int phase, index;
    float time;
    Animator animator_;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //animator_ = animator;
        animator.SetInteger("AnimationType", 3);
        ///index = animator.GetLayerIndex("Spining");
        ///time = 0f;
        /*
        phase = 0;
        time = 0.0f;
        for (int i = 0; i < velocity.Length; i++) velocity[i] = 0;
        animator.SetFloat("Spin", 0.0f);
        animator.SetFloat("SpinR1", 0.0f);
        animator.SetFloat("SpinR2", 0.0f);
        animator.SetFloat("SpinRA", 0.0f);
        animator.SetFloat("SpinRS", 0.0f);
        */
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        ///time += Time.deltaTime;
        ///if (time > stateInfo.length - 0.4f) animator.SetInteger("AnimationType", 2);
        ///else animator.SetInteger("AnimationType", 3);

        /*
        time += Time.deltaTime;
        int route = animator.GetInteger("SpinRoute");
        switch (phase) {
            case 0:
                animator.SetFloat("Spin", Mathf.SmoothDamp(animator.GetFloat("Spin"), 1.0f, ref velocity[0], 0.5f));
                if (time > spin.length - 0.6f) Next(false);
                break;
            case 1:
                animator.SetFloat("Spin", Mathf.SmoothDamp(animator.GetFloat("Spin"), 0.0f, ref velocity[0], 0.3f));
                switch (route) {
                    case 0:
                    case 1:
                        animator.SetFloat("SpinR1", Mathf.SmoothDamp(animator.GetFloat("SpinR1"), 1.0f, ref velocity[1], 0.5f));
                        if (time > spinR1.length - 0.6f) Next(false);
                        break;
                    case 2:
                        animator.SetFloat("SpinRA", Mathf.SmoothDamp(animator.GetFloat("SpinRA"), 1.0f, ref velocity[2], 0.5f));
                        if (time > spinRA.length - 0.6f) Next(true);
                        break;
                    case 3:
                        animator.SetFloat("SpinRS", Mathf.SmoothDamp(animator.GetFloat("SpinRS"), 1.0f, ref velocity[3], 0.5f));
                        if (time > spinRS.length - 0.6f) Next(true);
                        break;
                    case 4:
                    case 5:
                        animator.SetFloat("SpinR2", Mathf.SmoothDamp(animator.GetFloat("SpinR2"), 1.0f, ref velocity[4], 0.5f));
                        if (time > spinR2.length - 0.6f) Next(false);
                        break;
                }
                break;
            case 2:
                animator.SetFloat("SpinR1", Mathf.SmoothDamp(animator.GetFloat("SpinR1"), 0.0f, ref velocity[1], 0.3f));
                animator.SetFloat("SpinR2", Mathf.SmoothDamp(animator.GetFloat("SpinR2"), 0.0f, ref velocity[4], 0.3f));
                switch (route) {
                    case 0:
                    case 4:
                        animator.SetFloat("SpinRA", Mathf.SmoothDamp(animator.GetFloat("SpinRA"), 1.0f, ref velocity[2], 0.5f));
                        if (time > spinRA.length - 0.6f) Next(true);
                        break;
                    case 1:
                    case 5:
                        animator.SetFloat("SpinRS", Mathf.SmoothDamp(animator.GetFloat("SpinRS"), 1.0f, ref velocity[3], 0.5f));
                        if (time > spinRS.length - 0.6f) Next(true);
                        break;
                }
                break;
        }
        if (animator.GetFloat("Spin") > 0.999f) animator.SetFloat("Spin", 1.0f);
        if (animator.GetFloat("SpinR1") > 0.999f) animator.SetFloat("SpinR1", 1.0f);
        if (animator.GetFloat("SpinR2") > 0.999f) animator.SetFloat("SpinR2", 1.0f);
        if (animator.GetFloat("SpinRA") > 0.999f) animator.SetFloat("SpinRA", 1.0f);
        if (animator.GetFloat("SpinRS") > 0.999f) animator.SetFloat("SpinRS", 1.0f);
        if (animator.GetFloat("Spin") < 0.001f) animator.SetFloat("Spin", 0.0f);
        if (animator.GetFloat("SpinR1") < 0.001f) animator.SetFloat("SpinR1", 0.0f);
        if (animator.GetFloat("SpinR2") < 0.001f) animator.SetFloat("SpinR2", 0.0f);
        if (animator.GetFloat("SpinRA") < 0.001f) animator.SetFloat("SpinRA", 0.0f);
        if (animator.GetFloat("SpinRS") < 0.001f) animator.SetFloat("SpinRS", 0.0f);
        */
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }

    void Next(bool end) {
        phase++;
        for (int i = 0; i < velocity.Length; i++) velocity[i] = 0;
        time = 0.0f;
        if (end) animator_.SetInteger("AnimationType", 2);
    }
}
