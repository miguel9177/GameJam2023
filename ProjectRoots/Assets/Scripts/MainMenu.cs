using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject settingsCanvas;
    public GameObject controlsCanvas;
    // Start is called before the first frame update


    private void Start()
    {
        //SoundManager.instance.PlaySound(,1f)
    }


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
        mainCanvas.SetActive(true);
    }

    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }
}
