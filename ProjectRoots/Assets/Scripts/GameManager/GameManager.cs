using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float timeToWaitForTimeTravelToFinish;
    public float timeTravelCooldown = 2;

    public bool isOnPast;
    private bool isTimeTraveling = false;

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
    public Action<bool> OnBeginTimeTravel;
    //if false go to future, if true go to past
    public Action<bool> OnTimeTravel;
    public Action OnForcedDropObject;
    #endregion

    private void Start()
    {
        InputManager.Instance.OnPressedR += OnPressedR;
        Invoke("LateStart", 0.1f);
    }

    private void OnPressedR()
    {
        
        StartCoroutine(TimeTravel());
    }

    private void LateStart()
    {
        OnTimeTravel?.Invoke(isOnPast);
    }

    private IEnumerator TimeTravel()
    {
        if (isTimeTraveling)
            yield break;

        isTimeTraveling = true;
        isOnPast = !isOnPast;
        OnBeginTimeTravel?.Invoke(isOnPast);
        yield return new WaitForSeconds(timeToWaitForTimeTravelToFinish);
        OnTimeTravel?.Invoke(isOnPast);
        yield return new WaitForSeconds(timeTravelCooldown);
        isTimeTraveling = false;
    }

    public void ForceDropObject()
    {
        OnForcedDropObject?.Invoke();
    }
    
}
