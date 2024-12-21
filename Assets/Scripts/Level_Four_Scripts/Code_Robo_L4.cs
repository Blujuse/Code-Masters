using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Code_Robo_L4 : MonoBehaviour
{
    [Header("Robot Positions")]
    [SerializeField] private Transform RobotIdlePos;
    [SerializeField] private Transform RobotIdleLook;
    [SerializeField] private Transform PuzzleOnePosObj;
    [SerializeField] private Transform PuzzleTwoPosObj;
    public bool IfAtPuzzlePos = false;
    public bool IfAtPuzzlePosTwo = false;

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
    [HideInInspector] public GameObject PuzzleCanvasTwo;

    [Header("Robot Dialogue")]
    public GameObject DialoguePanel;
    public TextMeshProUGUI DialogueText;
    public float WordSpeed;

    [Header("Puzzle Dialogue")]
    public string[] PuzzleDialogue;
    private int PuzzleIndex = 0;
    public bool HasDialogueSpoken = false;
    public bool PuzzleComplete = false;

    [Header("Puzzle Dialogue 2")]
    public string[] PuzzleDialogueTwo;
    private int PuzzleIndexTwo = 0;
    public bool HasDialogueSpokenTwo = false;

    [Header("Robot Interaction Canvas")]
    private GameObject InteractionCanvas;

    [Header("Audio References")]
    public Button_Press_Checks ButtonCheck1;
    public Button_Press_Checks ButtonCheck2;
    public AudioSource SoundMaker;
    public AudioClip TalkingSound;
    public AudioClip VictorySound;

    // Start is called before the first frame update
    void Start()
    {
        PuzzleCanvas = GameObject.FindWithTag("Puzzle_Canvas");

        PuzzleCanvasTwo = GameObject.FindWithTag("Puzzle_Canvas_2");

        BoxCol = GetComponent<BoxCollider>();

        Screen = GameObject.Find("/Programming Robot/Robo Screen");

        DialogueText.text = "";

        DialoguePanel.SetActive(false);

        HasDialogueSpoken = false;

        HasDialogueSpokenTwo = false;

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
        else if (IfAtPuzzlePosTwo == true)
        {
            PuzzleTwoPos();
        }
        else
        {
            RegularPos();
        }

        if (ButtonCheck1.Correct == true && ButtonCheck1.OnlyLockOnce == false)
        {
            SoundMaker.PlayOneShot(VictorySound, .2f);
        }

        if (ButtonCheck2.Correct == true && ButtonCheck2.OnlyLockOnce == false)
        {
            SoundMaker.PlayOneShot(VictorySound, .2f);
        }
    }

    // Regular Movement Section
    void RegularPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, RobotIdlePos.transform.position, Speed * Time.deltaTime);
        transform.LookAt(RobotIdleLook);

        Screen.SetActive(false);
        PuzzleCanvas.SetActive(false);
        PuzzleCanvasTwo.SetActive(false);
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

    // Puzzle Two Section
    void PuzzleTwoPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, PuzzleTwoPosObj.transform.position, Speed * Time.deltaTime);
        transform.LookAt(PlayerGlasses);

        if (transform.position == PuzzleTwoPosObj.transform.position && HasDialogueSpokenTwo == false)
        {
            Debug.Log("Arrived");

            if (!DialoguePanel.activeInHierarchy)
            {
                DialoguePanel.SetActive(true);
                StartCoroutine(PuzzleTwoTyping());
                PlayerController.StopMoving = true;
                SoundMaker.PlayOneShot(TalkingSound, .2f);
            }
            else if (DialogueText.text == PuzzleDialogueTwo[PuzzleIndexTwo])
            {
                SoundMaker.Stop();
                if (Input.GetKey(KeyCode.Q))
                {
                    PuzzleTwoNextLine();
                }              
            }
        }
        else
        {
            DialoguePanel.SetActive(false);
        }
    }

    IEnumerator PuzzleTwoTyping()
    {
        foreach (char letter in PuzzleDialogueTwo[PuzzleIndexTwo].ToCharArray())
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

    public void PuzzleTwoNextLine()
    {
        if (PuzzleIndexTwo < PuzzleDialogueTwo.Length - 1)
        {
            PuzzleIndexTwo++;
            DialogueText.text = "";
            StartCoroutine(PuzzleTwoTyping());
            SoundMaker.PlayOneShot(TalkingSound, .2f);
        }
        else
        {
            DialogueText.text = "";
            Screen.SetActive(true);
            BoxCol.enabled = true;
            HasDialogueSpokenTwo = true;
            PlayerController.StopMoving = false;
            SoundMaker.Stop();
        }
    }

    // Triggers
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E) && PuzzleComplete == false)
        {
            PuzzleCanvas.SetActive(true);
            InteractionCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerController.StopMoving = true;
        }

        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E) && PuzzleComplete == true)
        {
            PuzzleCanvasTwo.SetActive(true);
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
