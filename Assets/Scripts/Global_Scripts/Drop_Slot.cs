using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop_Slot : MonoBehaviour, IDropHandler
{ // IDropHandler is used to handle drag and drop events, is is taken from the EventSystems libary
    [Header("Robot References")]
    private GameObject Robot;
    private Code_Robo_L1 RobotScript1;
    private Code_Robo_L2 RobotScript2;
    
    [Header("Answers")]
    public GameObject[] CAnswer;
    public GameObject[] WAnswer;
    public GameObject Cross;
    public bool Correct = false;

    [Header("Audio References")]
    public AudioSource SoundMaker;
    public AudioClip FailSound;
    public bool HasFailSoundPlayed;

    private void Start()
    {
        Cross.SetActive(false);
        Correct = false;

        Robot = GameObject.FindWithTag("Robot");

        RobotScript1 = Robot.GetComponent<Code_Robo_L1>();
        RobotScript2 = Robot.GetComponent<Code_Robo_L2>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        if (RobotScript1 != null)
        {
            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Correct")) //if there is an item above it then it sets that item to this ones position
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                RobotScript1.PuzzleCanvas.SetActive(false);
                RobotScript1.IfAtPuzzlePos = false;
                Correct = true;
            }
            else if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Wrong"))
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                Cross.SetActive(true);

                if (HasFailSoundPlayed == false)
                {
                    SoundMaker.PlayOneShot(FailSound, .2f);
                    HasFailSoundPlayed = true;
                }
            }
        }
    
        if (RobotScript2 != null)
        {
            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Correct")) //if there is an item above it then it sets that item to this ones position
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                RobotScript2.PuzzleCanvas.SetActive(false);
                RobotScript2.IfAtPuzzlePos = false;
                Correct = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Wrong"))
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                Cross.SetActive(true);

                if (HasFailSoundPlayed == false)
                {
                    SoundMaker.PlayOneShot(FailSound, .2f);
                    HasFailSoundPlayed = true;
                }
            }
        }
    }
}
