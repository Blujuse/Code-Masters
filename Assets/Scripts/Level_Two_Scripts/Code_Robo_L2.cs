using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Code_Robo_L2 : MonoBehaviour
{
    [Header("Robot Positions")]
    [SerializeField] private Transform RobotIdlePos;
    [SerializeField] private Transform RobotIdleLook;
    [SerializeField] private Transform PuzzleOnePosObj;
    [SerializeField] private Transform RobotMOPos;
    public bool IfAtPuzzlePos = false;
    public bool IfAtMOPos = false;

    [Header("Player References")]
    [SerializeField] private Transform PlayerGlasses;
    private GameObject Player;
    private Character_Controller PlayerController;

    [Header("Robot References")]
    private GameObject Screen;
    private BoxCollider BoxCol;
    [SerializeField] private float Speed = 1f;

    [Header("Canvas")]
    [HideInInspector] public GameObject PuzzleCanvas;

    [Header("Robot Dialogue")]
    public GameObject DialoguePanel;
    public TextMeshProUGUI DialogueText;
    public float WordSpeed;

    [Header("MO Dialogue")]
    public string[] MODialogue;
    private int MOIndex = 0;

    [Header("Puzzle Dialogue")]
    public string[] PuzzleDialogue;
    private int PuzzleIndex = 0;
    public bool HasDialogueSpoken = false;

    [Header("Robot Interaction Canvas")]
    private GameObject InteractionCanvas;

    [Header("Audio References")]
    public AudioSource SoundMaker;
    public AudioClip TalkingSound;

    private void Start()
    {
        PuzzleCanvas = GameObject.FindWithTag("Puzzle_Canvas");

        BoxCol = GetComponent<BoxCollider>();

        Screen = GameObject.Find("/Programming Robot/Robo Screen");

        DialogueText.text = "";

        DialoguePanel.SetActive(false);

        IfAtMOPos = false;

        HasDialogueSpoken = false;

        Player = GameObject.Find("Player");

        PlayerController = Player.GetComponent<Character_Controller>();

        InteractionCanvas = GameObject.FindWithTag("Interaction Canvas");

        InteractionCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IfAtPuzzlePos == true)
        {
            PuzzleOnePos();
        }
        else if (IfAtMOPos == true)
        {
            MOPos();
        }
        else
        {
            RegularPos();
        }
    }

    // Regular Movement Section
    void RegularPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, RobotIdlePos.transform.position, Speed * Time.deltaTime);
        transform.LookAt(RobotIdleLook);

        Screen.SetActive(false);
        PuzzleCanvas.SetActive(false);
        DialoguePanel.SetActive(false);

        BoxCol.enabled = false;

        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/

        if (PlayerController.PlayerInWinZone == false)
        {
            PlayerController.StopMoving = false;
        }
        else if (PlayerController.PlayerInWinZone == true)
        {
            PlayerController.StopMoving = true;
        }

    }

    // Puzzle One Section
    void PuzzleOnePos()
    {
        transform.position = Vector3.MoveTowards(transform.position, PuzzleOnePosObj.transform.position, Speed * Time.deltaTime);
        transform.LookAt(PlayerGlasses);

        if (transform.position == PuzzleOnePosObj.transform.position && HasDialogueSpoken == false)
        {
            Debug.Log("Arrived");

            if (!DialoguePanel.activeInHierarchy)
            {
                DialoguePanel.SetActive(true);
                StartCoroutine(PuzzleTyping());
                PlayerController.StopMoving = true;
                SoundMaker.PlayOneShot(TalkingSound, .2f);
            }
            else if (DialogueText.text == PuzzleDialogue[PuzzleIndex])
            {
                SoundMaker.Stop();

                if (Input.GetKey(KeyCode.Q))
                {
                    PuzzleNextLine();
                }
            }
        }
        else
        {
            DialoguePanel.SetActive(false);
        }
    }

    IEnumerator PuzzleTyping()
    {
        foreach (char letter in PuzzleDialogue[PuzzleIndex].ToCharArray())
        {
            DialogueText.text += letter;

            if (Input.GetKey(KeyCode.Q))
            {
                yield return new WaitForSeconds(0.0000000000000001f);
            }
            else
            {
                yield return new WaitForSeconds(WordSpeed);
            }
        }
    }

    public void PuzzleNextLine()
    {
        if (PuzzleIndex < PuzzleDialogue.Length - 1)
        {
            PuzzleIndex++;
            DialogueText.text = "";
            StartCoroutine(PuzzleTyping());
            SoundMaker.PlayOneShot(TalkingSound, .2f);
        }
        else
        {
            DialogueText.text = "";
            Screen.SetActive(true);
            BoxCol.enabled = true;
            HasDialogueSpoken = true;
            PlayerController.StopMoving = false;
            SoundMaker.Stop();
        }
    }

    // Tutorial Dialogue Section
    void MOPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, RobotMOPos.transform.position, Speed * Time.deltaTime);
        transform.LookAt(PlayerGlasses);

        if (transform.position == RobotMOPos.transform.position)
        {
            Debug.Log("Arrived");
            if (!DialoguePanel.activeInHierarchy)
            {
                DialoguePanel.SetActive(true);
                StartCoroutine(MOTyping());
                PlayerController.StopMoving = true;
                SoundMaker.PlayOneShot(TalkingSound, .2f);
            }
            else if (DialogueText.text == MODialogue[MOIndex])
            {
                SoundMaker.Stop();
                if (Input.GetKey(KeyCode.Q))
                {
                    MONextLine();
                }              
            }
        }

        Screen.SetActive(false);
        PuzzleCanvas.SetActive(false);

        BoxCol.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator MOTyping()
    {
        foreach (char letter in MODialogue[MOIndex].ToCharArray())
        {
            DialogueText.text += letter;

            if (Input.GetKey(KeyCode.Q))
            {
                yield return new WaitForSeconds(0.0000000000000001f);
            }
            else
            {
                yield return new WaitForSeconds(WordSpeed);
            }
        }
    }

    public void MONextLine()
    {
        if (MOIndex < MODialogue.Length - 1)
        {
            MOIndex++;
            DialogueText.text = "";
            StartCoroutine(MOTyping());
            SoundMaker.PlayOneShot(TalkingSound, .2f);
        }
        else
        {
            IfAtMOPos = false;
            DialogueText.text = "";
            SoundMaker.Stop();
        }
    }

    // Triggers
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            PuzzleCanvas.SetActive(true);
            InteractionCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerController.StopMoving = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionCanvas.SetActive(false);
        }
    }
}