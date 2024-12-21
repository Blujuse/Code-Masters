using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("New Checkpoint");
            Checkpoints_Player.LastCheckpointPos = other.transform.position;
        }
    }   

}
