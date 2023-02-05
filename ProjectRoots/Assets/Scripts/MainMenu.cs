using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject settingsCanvas;
    public GameObject controlsCanvas;
    public GameObject historyCanvas;

    public GameObject musicScrollbar;
    public GameObject vfxScrollbar;

    public static float vfxVolume;
    public static float musicVolume;

    public void Play()
    {
        //Debug.Log("Pressed");
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }


    public void Back()
    {
        settingsCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        historyCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        
    }

    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void OpenControls()
    {
        controlsCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void openHistory()
    {
        historyCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void changeMusicScrollbar()
    {
        Debug.Log(musicVolume);
        musicVolume = musicScrollbar.GetComponent<Scrollbar>().value;
    }

    public void changeVFXScrollbar() { 
        vfxVolume= vfxScrollbar.GetComponent<Scrollbar>().value;
        //Debug.Log(vfxVolume);
    }
}
