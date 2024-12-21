using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy_Detection : MonoBehaviour
{
    [Header("Private Variables")]
    private MeshRenderer ThisObjMesh;

    [Header("Slider")]
    public Slider AudioSlider;

    private void Start()
    {
        ThisObjMesh = gameObject.GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (AudioSlider.value == 0)
        {
            Debug.Log("Player Can Walk Through");
            ThisObjMesh.enabled = false;
        }
        else
        {
            Debug.Log("Player Can Walk Through");
            ThisObjMesh.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (AudioSlider.value > 0)
        {            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
