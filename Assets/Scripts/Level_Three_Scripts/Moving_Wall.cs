using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Wall : MonoBehaviour
{
    [Header("Movement")]
    public float Speed = 3;

    [Header("Waypoints")]
    public Transform EndPoint;

    [Header("Booleans")]
    public static bool IsPuzzleWon;

    [Header("Audio References")]
    public AudioSource SoundMaker;
    private bool HasSoundPlayed;

    private void Start()
    {
        IsPuzzleWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPuzzleWon == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, EndPoint.position, Speed * Time.deltaTime);

            if (HasSoundPlayed == false)
            {
                SoundMaker.Play();
                HasSoundPlayed = true;
            }
        }
    }
}
