using UnityEngine;

public class AnimationChanger : MonoBehaviour {
    public Animator animator;
    public string eventName;



    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (Input.anyKeyDown) animator.SetTrigger("Click");
    }
}

