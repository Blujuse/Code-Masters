using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3_Puzzle_One_Trigger : MonoBehaviour
{
    [Header("Robot References")]
    public Code_Robo_L3 Robot;

    [Header("Trigger")]
    private BoxCollider ThisObjCol;

    private void Start()
    {
        ThisObjCol = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Robot.IfAtPuzzlePos = true;

            ThisObjCol.enabled = false;
        }
    }
}
