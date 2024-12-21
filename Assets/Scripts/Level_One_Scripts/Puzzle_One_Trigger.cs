using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_One_Trigger : MonoBehaviour
{
    [Header("Robot")]
    private GameObject Robot;
    private Code_Robo_L1 RobotScript;

    [Header("Trigger")]
    private BoxCollider ThisObjCol;

    private void Start()
    {
        Robot = GameObject.FindWithTag("Robot");

        RobotScript = Robot.GetComponent<Code_Robo_L1>();

        ThisObjCol = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RobotScript.IfAtPuzzlePos = true;

            ThisObjCol.enabled = false;
        }
    }
}
