using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform StartPoint;
    public Transform EndPoint;
    
    [Header("Movement Speed/Time")]
    public float TravelTime;
    public float Speed;
    public bool Bounceback;

    [Header("Drag Drop Refrences")]
    [SerializeField] private Drop_Slot DropPoint;
    [SerializeField] private GameObject CAnswer;

    [Header("Player References")]
    private GameObject Player;
    private Character_Controller PlayerScript;

    [Header("Reparent Timer")]
    public float ReparentTime;
    private float ReparentCounter;

    [Header("Audio References")]
    public AudioSource SoundMaker;
    public AudioClip VictorySound;
    private bool HasVictorySoundPlayed;
    public AudioSource MovingPlatformSoundMaker;

    [Header("Pause Menu References")]
    public New_Pause_Menu PauseScript;

    private void Start()
    {
        CAnswer = GameObject.FindWithTag("Drop_Point");

        DropPoint = CAnswer.GetComponent<Drop_Slot>();

        Player = GameObject.FindWithTag("Player");

        PlayerScript = Player.GetComponent<Character_Controller>();
    }

    private void Update()
    {
        if (PlayerScript.Grounded == true)
        {
            ReparentCounter = ReparentTime;
        }
        else if (PlayerScript.Grounded == false)
        {
           ReparentCounter -= Time.deltaTime;
        }

        if (DropPoint.Correct == true && PauseScript.IsPaused == false)
        {
            if (HasVictorySoundPlayed == false)
            {
                SoundMaker.PlayOneShot(VictorySound, .2f);
                HasVictorySoundPlayed = true;
            }

            if (!MovingPlatformSoundMaker.isPlaying)
            {
                MovingPlatformSoundMaker.Play();
            }
        }
        else
        {
            MovingPlatformSoundMaker.Stop();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DropPoint.Correct == true && Bounceback == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, EndPoint.position, Speed * Time.deltaTime);
            if (transform.position == EndPoint.position)
            {
                Bounceback = true;
            }            
        }
        else if (Bounceback == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, StartPoint.position, Speed * Time.deltaTime);
            if (transform.position == StartPoint.position)
            {
                Bounceback = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {       
        if (other.CompareTag("Player"))
        {
            Debug.Log("EnterTrigger");
            if (ReparentCounter >= 1f)
            {
                other.transform.parent = transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            Debug.Log("ExitTrigger");
            other.transform.parent = null;
        }
    }

}
