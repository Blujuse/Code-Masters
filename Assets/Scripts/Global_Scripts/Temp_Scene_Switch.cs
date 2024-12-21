using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp_Scene_Switch : MonoBehaviour
{
    [Header("Player References")]
    private Character_Controller PlayerScript;
    private GameObject Player;
    
    [Header("Timer")]
    public float SceneChangeTime;
    private float SceneChangeTimer;
    private bool TimerOn = false;

    [Header("Audio References")]
    public AudioSource SoundMaker;
    private bool HasPowerOffPlayed;

    private void Start()
    {
        Player = GameObject.Find("Player");
        PlayerScript = Player.GetComponent<Character_Controller>();

        SceneChangeTime = 3f;

        SceneChangeTimer = SceneChangeTime;

        TimerOn = false;
    }

    private void Update()
    {
        if (TimerOn)
        {
            Timer();
            PlayerScript.StopMoving = true;
        }

        if (SceneChangeTimer <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TimerOn = true;

            PlayerScript.PlayerAnim.SetTrigger("PowerDownAnim");

            PlayerScript.PlayerInWinZone = true;

            if (HasPowerOffPlayed == false)
            {
                SoundMaker.Play();
                HasPowerOffPlayed = true;
            }            
        }
    }

    void Timer()
    {
        SceneChangeTimer -= Time.deltaTime;
    }
}
