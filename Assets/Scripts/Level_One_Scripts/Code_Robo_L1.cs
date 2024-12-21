using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Code_Robo_L1 : MonoBehaviour
{
    [Header("Robot Positions")]
    [SerializeField] private Transform RobotIdlePos;
    [SerializeField] private Transform RobotIdleLook;
    [SerializeField] private Transform PuzzleOnePosObj;
    [SerializeField] private Transform RobotTutorialPos;
    [SerializeField] private Transform JumpPos;
    public bool IfAtPuzzlePos = false;
    public bool IfAtTutorialPos = false;
    public bool IfAtJumpPos = false;

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

    [Header("Tutorial Dialogue")]
    public string[] TutorialDialogue;
    private int TutorialIndex = 0;
    
    [Header("Jump Dialogue")]
    public string[] JumpDialogue;
    private int JumpIndex = 0;

    [Header("Puzzle Dialogue")]
    public string[] PuzzleDialogue;
    private int PuzzleIndex = 0;
    public bool HasDialogueSpoken = false;

    [Header("Robot Interaction Canvas")]
    private GameObject InteractionCanvas;

    [Header("Pause Menu")]
    private New_Pause_Menu PauseCanvasScript;

    [Header("Audio References")]
    public AudioSource SoundMaker;
    public AudioClip TalkingSound;
    public AudioClip VictorySound;
    public bool HasVictorySoundPlayed;

    [Header("Drop_Slot_Script")]
    public Drop_Slot DropSlot;

    private void Start()
    {
        PuzzleCanvas = GameObject.FindWithTag("Puzzle_Canvas");

        BoxCol = GetComponent<BoxCollider>();

        Screen = GameObject.Find("/Programming Robot/Robo Screen");

        DialogueText.text = "";
        DialoguePanel.SetActive(false);

        IfAtTutorialPos = true;
        HasDialogueSpoken = false;

        Player = GameObject.Find("Player");
        PlayerController = Player.GetComponent<Character_Controller>();

        InteractionCanvas = GameObject.FindWithTag("Interaction Canvas");
        InteractionCanvas.SetActive(false);

        PauseCanvasScript = Player.GetComponent<New_Pause_Menu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IfAtPuzzlePos == true)
        {
            PuzzleOnePos();
        }
        else if (IfAtJumpPos == true)
        {
            JumpDialoguePos();
        }
        else if (IfAtTutorialPos == true)
        {
            TutorialPos();
        }
        else
        {
            RegularPos();
        }    
        
        if (DropSlot.Correct == true)
        {
            if (HasVictorySoundPlayed == false)
            {
                SoundMaker.PlayOneShot(VictorySound, .2f);
                HasVictorySoundPlayed = true;
            }
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

        PauseCanvasScript.InPuzzle = false;

        if (PauseCanvasScript.IsPaused == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

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
    void TutorialPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, RobotTutorialPos.transform.position, Speed * Time.deltaTime);
        transform.LookAt(PlayerGlasses);
        
        if (transform.position == RobotTutorialPos.transform.position)
        {
            Debug.Log("Arrived");
            if (!DialoguePanel.activeInHierarchy)
            {
                DialoguePanel.SetActive(true);
                StartCoroutine(TutorialTyping());
                PlayerController.StopMoving = true;
                SoundMaker.PlayOneShot(TalkingSound, .2f);

            }
            else if (DialogueText.text == TutorialDialogue[TutorialIndex])
            {
                SoundMaker.Stop();
                if (Input.GetKey(KeyCode.Q))
                {
                    TutorialNextLine();
                }
            }        
        }

        Screen.SetActive(false);
        PuzzleCanvas.SetActive(false);
       
        BoxCol.enabled = false;
    }

    IEnumerator TutorialTyping()
    {
        foreach (char letter in TutorialDialogue[TutorialIndex].ToCharArray())
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

    public void TutorialNextLine()
    {
        if (TutorialIndex < TutorialDialogue.Length - 1)
        {
            TutorialIndex++;
            DialogueText.text = "";
            StartCoroutine(TutorialTyping());
            SoundMaker.PlayOneShot(TalkingSound, .2f);
        }
        else
        {
            IfAtTutorialPos = false;
            DialogueText.text = "";
            SoundMaker.Stop();
        }
    }

    // Jump Dialogue Section
    void JumpDialoguePos()
    {
        transform.position = Vector3.MoveTowards(transform.position, JumpPos.transform.position, Speed * Time.deltaTime);
        transform.LookAt(PlayerGlasses);
       
        if (transform.position == JumpPos.transform.position)
        {
            Debug.Log("Arrived");
            if (!DialoguePanel.activeInHierarchy)
            {
                DialoguePanel.SetActive(true);
                StartCoroutine(JumpTyping());
                PlayerController.StopMoving = true;
                SoundMaker.PlayOneShot(TalkingSound, .2f);
            }
            else if (DialogueText.text == JumpDialogue[JumpIndex])
            {
                SoundMaker.Stop();
                if (Input.GetKey(KeyCode.Q))
                {
                    JumpNextLine();
                }
            }
        }
        else
        {
            PlayerController.StopMoving = false;
        }

        Screen.SetActive(false);
        PuzzleCanvas.SetActive(false);
       
        BoxCol.enabled = false;
    }

    IEnumerator JumpTyping()
    {
        foreach (char letter in JumpDialogue[JumpIndex].ToCharArray())
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

    public void JumpNextLine()
    {
        if (JumpIndex < JumpDialogue.Length - 1)
        {
            JumpIndex++;
            DialogueText.text = "";
            StartCoroutine(JumpTyping());
            SoundMaker.PlayOneShot(TalkingSound, .2f);
        }
        else
        {
            IfAtJumpPos = false;
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

            PlayerController.StopMoving = true;
            
            InteractionCanvas.SetActive(false);
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            PauseCanvasScript.InPuzzle = true;
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
