using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestValueManager : MonoBehaviour {
    public GameObject model;
    public GameObject mSlider, eSlider, sSlider;
    public GameObject animeTypeField, spinRouteField;
    public GameObject animeTypeCheck, spinRouteCheck;

    int[] index;
    Slider _mSlider, _eSlider, _sSlider;
    InputField aT_field, sR_feild;
    Toggle aT_toggle, sR_toggle;
    Animator animator;
    // Start is called before the first frame update
    void Start() {
        animator = model.GetComponent<Animator>();
        _mSlider = mSlider.GetComponent<Slider>();
        _eSlider = eSlider.GetComponent<Slider>();
        _sSlider = sSlider.GetComponent<Slider>();
        index = new int[] { animator.GetLayerIndex("Motion"), animator.GetLayerIndex("Expression"), animator.GetLayerIndex("Spining") };

        aT_field = animeTypeField.GetComponent<InputField>();
        sR_feild = spinRouteField.GetComponent<InputField>();

        aT_toggle = animeTypeCheck.GetComponent<Toggle>();
        sR_toggle = spinRouteCheck.GetComponent<Toggle>();

    }

    // Update is called once per frame
    void LateUpdate() {
        _mSlider.value = animator.GetLayerWeight(index[0]);
        _eSlider.value = animator.GetLayerWeight(index[1]);
        _sSlider.value = animator.GetLayerWeight(index[2]);

        

        if (aT_toggle.isOn) {
            aT_field.interactable = true;
            animator.SetInteger("AnimationType", int.Parse(aT_field.text));
        }
        else {
            aT_field.interactable = false;
            aT_field.text = animator.GetInteger("AnimationType").ToString();
        }
        if (sR_toggle.isOn) {
            sR_feild.interactable = true;
            if (int.Parse(sR_feild.text) > 5) sR_feild.text = "5";
            else if (int.Parse(sR_feild.text) < 0) sR_feild.text = "0";
            animator.SetInteger("SpinRoute", int.Parse(sR_feild.text));
        }
        else {
            sR_feild.interactable = false;
            sR_feild.text = animator.GetInteger("SpinRoute").ToString();
        }

    }

    void Range(int i, int max, int min) {
        if (i > max) i = max;
        if (i < min) i = min;
    }
}
