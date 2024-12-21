using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class New_Pause_Menu : MonoBehaviour
{
    [Header("Pause Variables")]
    public bool IsPaused = false;
    public GameObject PauseMenuCanvas;
    public bool InPuzzle = false;
    
    [Header("Cam Variables")]
    public GameObject ThirdPersonCamera;

    [Header("Other Menus")]
    public GameObject SettingsMenu;
    public GameObject MainPauseMenu;

    [Header("Audio Slider")]
    public Slider AudioSlider;

    [Header("Audio References")]
    public AudioSource SoundMaker;

    private void Start()
    {
        PauseMenuCanvas = GameObject.FindWithTag("Pause Menu Canvas");

        PauseMenuCanvas.SetActive(false);

        ThirdPersonCamera = GameObject.FindWithTag("Third Person Cam");

        AudioSlider.value = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsPaused == false && !InPuzzle)
        {
            Time.timeScale = 0;
            
            IsPaused = true;
            PauseMenuCanvas.SetActive(true);
           
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            ThirdPersonCamera.SetActive(false);

            SettingsMenu.SetActive(false);

            MainPauseMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && IsPaused == true && !InPuzzle)
        {
            Time.timeScale = 1;
            
            IsPaused = false;
            PauseMenuCanvas.SetActive(false);
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            ThirdPersonCamera.SetActive(true);

            SettingsMenu.SetActive(false);

            MainPauseMenu.SetActive(true);
        }
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;

        IsPaused = false;
        PauseMenuCanvas.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ThirdPersonCamera.SetActive(true);

        SoundMaker.Play();
    }

    public void OptionsButton()
    {
        SettingsMenu.SetActive(true);
        MainPauseMenu.SetActive(false);

        SoundMaker.Play();
    }

    public void ReturnToMainPauseButton()
    {
        SettingsMenu.SetActive(false);
        MainPauseMenu.SetActive(true);

        SoundMaker.Play();
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit Game");

        SoundMaker.Play();
    }

    public void OnAudioSliderChanged()
    {
        float volume = AudioSlider.value;
        AudioListener.volume = volume;
    }

}
