using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Press_Checks : MonoBehaviour
{
    [Header("Button Bools")]
    public bool Correct;
    public bool Wrong1;
    public bool Wrong2;

    [Header("Crosses")]
    public GameObject Cross1;
    public GameObject Cross2;

    [Header("Animations")]
    public Animator Crusher;

    [Header("Robot References")]
    public Code_Robo_L4 Robot;

    [Header("Lock The Mouse")]
    public bool OnlyLockOnce = false;

    [Header("Audio References")]
    public AudioSource SoundMaker;
    public AudioClip Click;
    public AudioClip FailSound;

    private void Start()
    {
        Correct = false;
        Wrong1 = false;
        Wrong2 = false;

        Crusher.SetBool("IsCrushing", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Correct == true && OnlyLockOnce == false)
        {
            Debug.Log("Correct");

            Crusher.SetBool("IsCrushing", false);

            Robot.IfAtPuzzlePos = false;
            Robot.IfAtPuzzlePosTwo = false;
            Robot.PuzzleComplete = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            OnlyLockOnce = true;
        }

        if (Wrong1 == true)
        {
            Cross1.SetActive(true);
        }
        else
        {
            Cross1.SetActive(false);
        }

        if (Wrong2 == true)
        {
            Cross2.SetActive(true);
        }
        else
        {
            Cross2.SetActive(false);
        }
    }

    public void CorrectOnClick()
    {
        Correct = true;
        SoundMaker.PlayOneShot(Click, .2f);
    }

    public void WrongOneOnClick()
    {
        Wrong1 = true;
        SoundMaker.PlayOneShot(FailSound, .2f);
    }

    public void WrongTwoOnClick()
    {
        Wrong2 = true;
        SoundMaker.PlayOneShot(FailSound, .2f);
    }
}
