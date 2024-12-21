using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag_Drop_Programming : MonoBehaviour, IDropHandler
{
    [Header("Pug Puzzle 1 References")]
    public GameObject Plug;
    public Grid_Movement PlugScript;

    [Header("Pug Puzzle 2 References")]
    public GameObject Plug2;
    public Grid_Movement PlugScript2;

    [Header("References")]
    public Code_Robo_L3 Robot;

    private void Start()
    {
        Plug = GameObject.FindWithTag("Plug");

        PlugScript = Plug.GetComponent<Grid_Movement>();

        Plug2 = GameObject.FindWithTag("Plug_Two");

        PlugScript2 = Plug2.GetComponent<Grid_Movement>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        if (Robot.IfAtPuzzlePos == true)
        {
            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("CodeBlock_Up"))
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                PlugScript.StartCoroutine(PlugScript.MovePlayer(Vector3.up * PlugScript.KeyMovementIncrease));
            }

            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("CodeBlock_Left"))
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                PlugScript.StartCoroutine(PlugScript.MovePlayer(Vector3.left * PlugScript.KeyMovementIncrease));
            }

            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("CodeBlock_Down"))
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                PlugScript.StartCoroutine(PlugScript.MovePlayer(Vector3.down * PlugScript.KeyMovementIncrease));
            }

            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("CodeBlock_Right"))
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                PlugScript.StartCoroutine(PlugScript.MovePlayer(Vector3.right * PlugScript.KeyMovementIncrease));
            }
        }

        if (Robot.IfAtPuzzlePosTwo == true)
        {
            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("CodeBlock_Up"))
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                PlugScript2.StartCoroutine(PlugScript2.MovePlayer(Vector3.up * PlugScript2.KeyMovementIncrease));
            }

            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("CodeBlock_Left"))
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                PlugScript2.StartCoroutine(PlugScript2.MovePlayer(Vector3.left * PlugScript2.KeyMovementIncrease));
            }

            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("CodeBlock_Down"))
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                PlugScript2.StartCoroutine(PlugScript2.MovePlayer(Vector3.down * PlugScript2.KeyMovementIncrease));
            }

            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("CodeBlock_Right"))
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                PlugScript2.StartCoroutine(PlugScript2.MovePlayer(Vector3.right * PlugScript2.KeyMovementIncrease));
            }
        }
    }
}