using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination_Lock : MonoBehaviour
{
    [Header("Movement")]
    public float Speed;

    [Header("Waypoints")]
    public Transform EndPoint;

    [Header("Private Variables")]
    private bool NearComboLock = false;
    private bool ComboComplete = false;

    // Update is called once per frame
    void Update()
    {
        if (NearComboLock == true && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.C))
        {
            ComboComplete = true;
        }
        
        if (ComboComplete == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, EndPoint.position, Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NearComboLock = true;
        }
    }
}
