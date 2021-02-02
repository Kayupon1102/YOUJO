//using System.Numerics;
using UnityEngine;
using Live2D.Cubism.Core;

public class AnimationManager : MonoBehaviour {
    public GameObject model;
    public GameObject eyeCentor;
    public float activeRange, damping, waightDump;

    Vector3 faceCentor, targetPosition, lastTargetPosition;
    Animator animator;
    int expressionIndex, spinIndex, motionIndex;
    bool isActive_, lastIsActive_;
    Vector3 velocity = Vector3.zero;
    float[] velocityF = new float[6];
    float[,] startParms;
    [SerializeField]bool rightMouseHold = false;
    [SerializeField] bool rightMouseDrag = false;
    Vector3 rightMousePos;
    float rightPressTime = 0;
    Vector3 ModelPosTmp;
    CubismParameter[] modelPrameter;


    void Start() {
        model.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0f));
        model.transform.position =
            new Vector3(model.transform.position.x - 2f, model.transform.position.y + 4f);
        animator = model.GetComponent<Animator>();
        faceCentor = eyeCentor.transform.position;
        targetPosition = Input.mousePosition;
        expressionIndex = animator.GetLayerIndex("Expression");
        spinIndex = animator.GetLayerIndex("Spining");
        motionIndex = animator.GetLayerIndex("Motion");

        animator.SetLayerWeight(expressionIndex, 0.0f);
        animator.SetLayerWeight(spinIndex, 0.0f);
        animator.SetLayerWeight(motionIndex, 1.0f);

        for (int i = 0; i < velocityF.Length; i++) velocityF[i] = 0;

        modelPrameter = model.GetComponent<CubismModel>().Parameters;
        startParms = new float[0,modelPrameter.Length];
    }

    void Update() {
        
        int animationType = animator.GetInteger("AnimationType");
        if (animationType == -1) {
            isActive_ = false;
            lastIsActive_ = false;
            animationType = 0;
            animator.SetInteger("AnimationType", 0);
        }
        if (animationType == 0 | animationType == 1) Chase();

        if (rightMouseHold&&rightPressTime<1) rightPressTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(1))
        {
            rightMouseHold = true;
            rightMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            rightMouseDrag = false;
            rightMouseHold = false;
            rightPressTime = 0;
        }
        if (rightMouseHold && !rightMouseDrag)
        {
            if(rightPressTime>=1 || rightMousePos!= Input.mousePosition)
            rightMouseDrag = true;
            ModelPosTmp = model.transform.position;
        }
        if (rightMouseDrag)
        {
            
            model.transform.position = ModelPosTmp + Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(rightMousePos);
        }
        //LayerWaight
        //AnimationType ==  0   レギュラーモーション、放置状態
        //                  1   マウス追尾
        //                  2
        //                  3   ぐるぐる

        //if (animationType == 0 || animationType == 1 ||animationType==-1) 
        //    animator.SetLayerWeight(motionIndex, Mathf.SmoothDamp(animator.GetLayerWeight(motionIndex), 1.0f, ref velocityF[0], waightDump));
        //else 
        //    animator.SetLayerWeight(motionIndex, Mathf.SmoothDamp(animator.GetLayerWeight(motionIndex), 0.0f, ref velocityF[1], waightDump));

        if (animationType == 1) 
            animator.SetLayerWeight(expressionIndex, Mathf.SmoothDamp(animator.GetLayerWeight(expressionIndex), 1.0f, ref velocityF[2], waightDump));
        else 
            animator.SetLayerWeight(expressionIndex, Mathf.SmoothDamp(animator.GetLayerWeight(expressionIndex), 0.0f, ref velocityF[3], waightDump));

        if (animationType == 3) 
            animator.SetLayerWeight(spinIndex, Mathf.SmoothDamp(animator.GetLayerWeight(spinIndex), 1.0f, ref velocityF[4], waightDump));
        else 
            animator.SetLayerWeight(spinIndex, Mathf.SmoothDamp(animator.GetLayerWeight(spinIndex), 0.0f, ref velocityF[5], waightDump));
    }

    float Range(float f, float min, float max) {
        if (f > max) return max;
        if (f < min) return min;
        return f;
    }

    void Chase() {
        lastTargetPosition = targetPosition;
        targetPosition = Input.mousePosition;
        targetPosition = Camera.main.ScreenToWorldPoint(targetPosition) - faceCentor;
        targetPosition.z = 0.0f;
        float distance = Vector3.Distance(targetPosition, Vector3.zero);
        isActive_ = distance < activeRange;
        //targetPosition = targetPosition / distance;
        if (!lastIsActive_ && isActive_) {//追尾開始時の処理
            animator.SetInteger("AnimationType", 1);
            //lastTargetPosition = Vector3.zero;
            animator.SetFloat("AngleSum", 0f);
            //Debug.Log("ON");
        }
        else if (lastIsActive_ && !isActive_) {//追尾終了時の処理
            animator.SetInteger("AnimationType", 0);
            //Debug.Log("OFF");
        }

        lastIsActive_ = isActive_;
        targetPosition = Vector3.SmoothDamp(lastTargetPosition, targetPosition, ref velocity, damping);

        //Debug.Log(targetPosition);

        animator.SetFloat("Dx", Range(targetPosition.x, -1f, 1f));
        animator.SetFloat("Dy", Range(targetPosition.y, -1f, 1f));
        if (isActive_) {
            animator.SetFloat(
                "AngleSum", Range(
                    animator.GetFloat("AngleSum") + Vector3.Angle(lastTargetPosition, targetPosition) - Time.deltaTime * 100.0f, 0, 2000));
        }
    }

    void BorderSmoother(AnimationClip next) {
        
    }
}
