using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISwich : MonoBehaviour
{
    public GameObject master, ui,inputbox;

    User user;
    float time;
    bool firstClick = false;

    void Start()
    {
        user = master.GetComponent<User>();
        time = 0;
    }

    public void Quit() {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }

    public void UiOff() {
        ui.SetActive(false);
    }

    void DoubleClick() {
        ui.SetActive(true);
    }


    void Update()
    {
        if (firstClick) {
            time += Time.deltaTime;
            if (time>0.2f) {
                time = 0.0f;
                firstClick = false;
            }
            else if(Input.GetMouseButtonDown(0)) {
                DoubleClick();
                firstClick = false;
            }
        }
        else {
            if (Input.GetMouseButtonDown(0)) firstClick = true;
        }
    }
}
