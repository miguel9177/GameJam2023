using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #region Events
    public Action OnPressedE = null;
    public Action OnPressedR = null;
    public Action OnPressedEscape = null;
    public Action OnPressedMouseLeftClick = null;
    
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnPressedE?.Invoke();
        }
        if(Input.GetKeyUp(KeyCode.R))
        {
            OnPressedR?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OnPressedEscape?.Invoke();
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnPressedMouseLeftClick?.Invoke();
        }
    }
}
