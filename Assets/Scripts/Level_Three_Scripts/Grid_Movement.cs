using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Movement : MonoBehaviour
{
    [Header("Hidden")]
    [HideInInspector] public bool IsMoving;
    [HideInInspector] public Vector3 OriginPos, TargetPos;
    [HideInInspector] public Vector3 CurrentPos;
    [HideInInspector] public Vector3 WinningPos;
    [HideInInspector] public Vector3 WinningPos2;
    private bool CursorLockFix = false;

    [Header("Movement References")]
    public float TimeToMove = 0.2f;
    public float KeyMovementIncrease;

    [Header("Plugs & Plug Goals")]
    public GameObject Plug1;
    public GameObject Plug2;
    public GameObject WinningObj;
    public GameObject WinningObj2;

    [Header("Robot References")]
    public Code_Robo_L3 Robot;

    private void Start()
    {
        WinningPos = WinningObj.transform.position;
        WinningPos2 = WinningObj2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Plug1.transform.position == WinningPos && CursorLockFix == false)
        {
            Robot.IfAtPuzzlePos = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Moving_Wall.IsPuzzleWon = true;
            Robot.PuzzleComplete = true;
            CursorLockFix = true;
        }

        if (Plug2.transform.position == WinningPos2)
        {
            Robot.IfAtPuzzlePosTwo = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Moving_Wall_2.IsPuzzleWon = true;
        }
    }

    public IEnumerator MovePlayer(Vector3 direction)
    {
        CurrentPos = transform.position;

        IsMoving = true;

        float elapsedTime = 0;

        OriginPos = transform.position;
        TargetPos = OriginPos + direction;

        while (elapsedTime < TimeToMove)
        {
            transform.position = Vector3.Lerp(OriginPos, TargetPos, (elapsedTime / TimeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = TargetPos;

        IsMoving = false;
    }
}
