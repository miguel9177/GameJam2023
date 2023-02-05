using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject playButton;
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
}
