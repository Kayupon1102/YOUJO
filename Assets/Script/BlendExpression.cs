using UnityEngine;

public class BlendExpression : MonoBehaviour {
    private Animator _blendTree;
    private int _expressionIndex;

    [SerializeField, Range(-1f, 5f)]
    public float Dx = 0f;

    [SerializeField, Range(-1f, 5f)]
    public float Dy = 0f;

    [SerializeField, Range(0f, 1f)]
    public float ExpressionWeight = 1f;

    void Start() {
        _blendTree = GetComponent<Animator>();

        _expressionIndex = _blendTree.GetLayerIndex("Expression");
    }

    void Update() {
        //Fail getting animator.
        if (_blendTree == null) {
            return;
        }


        //Setting Blend Param and Weights.
        _blendTree.SetFloat("Dx", Dx);
        _blendTree.SetFloat("Dy", Dy);

        if (_expressionIndex != -1)
            _blendTree.SetLayerWeight(_expressionIndex, ExpressionWeight);

    }
}