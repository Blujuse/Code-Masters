using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    [Header("Audio References")]
    public AudioSource SoundMaker;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnStartPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SoundMaker.Play();
    }

    public void OnQuitPress()
    {
        Application.Quit();
        Debug.Log("Game Quit");
        SoundMaker.Play();
    }
}
