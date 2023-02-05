using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    public Canvas escMenu;
    public Canvas canvas;
    private bool pressedEsc_1;
    private bool pressedEsc_2;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.OnPressedEscape += OnPressedEscape;
    }

    private void OnPressedEscape()
    {
        escMenu.gameObject.SetActive(!escMenu.isActiveAndEnabled);
        canvas.gameObject.SetActive(!canvas.isActiveAndEnabled);
        if (escMenu.isActiveAndEnabled)
        {
            PlayerCamera.stopWorking = true;
            PlayerMovement.stopWorking = true;
        }
        else
        {
            PlayerCamera.stopWorking = false;
            PlayerMovement.stopWorking = false;
        }           
    }    
}
