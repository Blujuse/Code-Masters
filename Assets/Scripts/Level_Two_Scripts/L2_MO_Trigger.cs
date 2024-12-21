using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2_MO_Trigger : MonoBehaviour
{
    [Header("Robot Reference")]
    private Code_Robo_L2 RobotScript;
    private GameObject RobotObj;

    [Header("Trigger")]
    private BoxCollider ThisObjCol;

    private void Start()
    {
        RobotObj = GameObject.FindWithTag("Robot");

        RobotScript = RobotObj.GetComponent<Code_Robo_L2>();

        ThisObjCol = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RobotScript.IfAtMOPos = true;

            ThisObjCol.enabled = false;
        }
    }
}
