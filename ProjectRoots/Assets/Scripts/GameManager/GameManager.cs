using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isOnPast;

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
    public Action<bool> OnTimeTravel;
    #endregion

    private void Start()
    {
        InputManager.Instance.OnPressedR += TimeTravel;
        Invoke("LateStart", 0.1f);
    }

    private void LateStart()
    {
        OnTimeTravel?.Invoke(isOnPast);
    }

    private void TimeTravel()
    {
        isOnPast = !isOnPast;
        OnTimeTravel?.Invoke(isOnPast);
    }
}
