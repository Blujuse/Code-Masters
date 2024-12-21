using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoints_Player : MonoBehaviour
{
    [Header("Checkpoints")]
    public static Vector3 LastCheckpointPos;

    [Header("Private Variables")]
    private static bool PlayerStartFix = false;

    private void Awake()
    {
        if (PlayerStartFix == false)
        {
            LastCheckpointPos = transform.position;           
        }

        transform.position = LastCheckpointPos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = LastCheckpointPos;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            PlayerStartFix = true;
        }
    }
}