using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Press_Programming : MonoBehaviour
{
    [Header("Pug Puzzle References")]
    public GameObject Plug;
    public Grid_Movement PlugScript;

    [Header("Audio References")]
    public AudioSource SoundMaker;

    private void Start()
    {
        Plug = GameObject.FindWithTag("Plug");

        PlugScript = Plug.GetComponent<Grid_Movement>();
    }

    public void PlugMoveUp()
    {
        if (PlugScript.IsMoving == false)
        {
            PlugScript.StartCoroutine(PlugScript.MovePlayer(Vector3.up * PlugScript.KeyMovementIncrease));

            SoundMaker.Play();
        }
    }

    public void PlugMoveDown()
    {
        if (PlugScript.IsMoving == false)
        {
            PlugScript.StartCoroutine(PlugScript.MovePlayer(Vector3.down * PlugScript.KeyMovementIncrease));

            SoundMaker.Play();
        }           
    }

    public void PlugMoveLeft()
    {
        if (PlugScript.IsMoving == false)
        {
            PlugScript.StartCoroutine(PlugScript.MovePlayer(Vector3.left * PlugScript.KeyMovementIncrease));

            SoundMaker.Play();
        }         
    }

    public void PlugMoveRight()
    {
        if (PlugScript.IsMoving == false)
        {
            PlugScript.StartCoroutine(PlugScript.MovePlayer(Vector3.right * PlugScript.KeyMovementIncrease));

            SoundMaker.Play();
        }           
    }

}
