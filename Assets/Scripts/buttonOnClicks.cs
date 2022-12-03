using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonOnClicks : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PauseMenu;
    public GameObject Continue;
    public GameObject Quit;
    public GameObject Options;


    public GameObject Controls;
    public GameObject Sound;
    public GameObject Back;

    public GameObject ControlsDisplay;
    public GameObject SoundDisplay;
    public GameObject[] AllMusic;

    private bool isEsc = false;
    void Start()
    {
        DisplayOriginalPause();
        Continue.GetComponent<Button>().onClick.AddListener(onClickContinue);
        Options.GetComponent<Button>().onClick.AddListener(onClickOptions);
        Controls.GetComponent<Button>().onClick.AddListener(DisplayConrolls);
        Quit.GetComponent<Button>().onClick.AddListener(onClickQuit);
        Back.GetComponent<Button>().onClick.AddListener(DisplayOriginalPause);
        onClickContinue();
    }

    // Update is called once per frame
    void Update()
    {
        isEsc = Input.GetKeyDown(KeyCode.Escape);
        if (isEsc == true)
        {
            if (PauseMenu.activeSelf)
            {
                if (ControlsDisplay.activeSelf || SoundDisplay.activeSelf)
                {
                    onClickOptions();
                }
                else if (!Continue.activeSelf) 
                {
                    DisplayOriginalPause();
                }
                else 
                {
                    onClickContinue();
                }
                
            }
            else
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
                //pause music
                foreach (GameObject music in AllMusic) {
                    music.GetComponent<AudioSource>().Pause();
                }
                

            }

        }
    }
    public void onClickContinue() {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        foreach (GameObject music in AllMusic)
        {
            music.GetComponent<AudioSource>().UnPause();
        }
    }
    public void onClickOptions()
    {
        setAllInactive();
        Sound.SetActive(true);
        Controls.SetActive(true);
        Back.SetActive(true);
    }
    public void onClickQuit()
    {
        SceneManager.LoadScene(0);
    }
    public void DisplayOriginalPause()
    {
        setAllInactive();
        Continue.SetActive(true);
        Quit.SetActive(true);
        Options.SetActive(true);
    }
    public void DisplayConrolls() 
    {
        setAllInactive();
        Back.SetActive(true);
        ControlsDisplay.SetActive(true);
    }

    private void setAllInactive()
    {
        Continue.SetActive(false);
        Quit.SetActive(false);
        Options.SetActive(false);
        Sound.SetActive(false);
        Controls.SetActive(false);
        Back.SetActive(false);
        ControlsDisplay.SetActive(false);
        SoundDisplay.SetActive(false);
    }
}
