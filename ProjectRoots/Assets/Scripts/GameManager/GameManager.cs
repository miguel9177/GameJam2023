using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isOnPast = true;

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
    //if false go to future, if true go to past
    public Action<bool> rewindButton;
    #endregion

    private void Start()
    {
        InputManager.Instance.OnPressedR += TimeTravel;
    }

    private void TimeTravel()
    {
        isOnPast = !isOnPast;
        rewindButton?.Invoke(isOnPast);
    }
}
