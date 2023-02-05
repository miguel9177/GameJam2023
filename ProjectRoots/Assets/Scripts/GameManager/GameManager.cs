using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float timeToWaitForTimeTravelToFinish;
    public float timeTravelCooldown = 2;
    public Image loseScreenPanel;
    public Image winScreenPanel;
    public string menuSceneName;

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
    public Action OnDropVaseAtBase;
    #endregion

    private void Start()
    {
        PlayerMovement.stopWorking = false;
        PlayerCamera.stopWorking = false;
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
        ForceDropObject();
        OnTimeTravel?.Invoke(isOnPast);
        yield return new WaitForSeconds(timeTravelCooldown);
        isTimeTraveling = false;
    }

    public void ForceDropObject()
    {
        OnForcedDropObject?.Invoke();
    }

    public void DropVaseAtBase()
    {
        OnDropVaseAtBase?.Invoke();
    }

    public void LostGame()
    {
        loseScreenPanel.gameObject.SetActive(true);
        PlayerMovement.stopWorking = true;
        PlayerCamera.stopWorking = true;
        StartCoroutine(RestartScene());
    }

    public void WonGame()
    {
        winScreenPanel.gameObject.SetActive(true);
        PlayerMovement.stopWorking = true;
        PlayerCamera.stopWorking = true;
        StartCoroutine(GoToMenu());
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    private IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(menuSceneName);
    }
}
