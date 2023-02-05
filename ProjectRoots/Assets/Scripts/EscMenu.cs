using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour
{
    public Canvas escMenu;
    public Canvas canvas;

    public Button restart;

    private bool pressedEsc_1;
    private bool pressedEsc_2;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.OnPressedEscape += OnPressedEscape;
    }

    private void OnPressedEscape()
    {
        Cursor.visible= true;
        escMenu.gameObject.SetActive(!escMenu.isActiveAndEnabled);
        canvas.gameObject.SetActive(!canvas.isActiveAndEnabled);
        if (escMenu.isActiveAndEnabled)
        {
            PlayerCamera.stopWorking = true;
            PlayerMovement.stopWorking = true;
        }
        else
        {
            Cursor.visible = false;
            PlayerCamera.stopWorking = false;
            PlayerMovement.stopWorking = false;
        }           
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
