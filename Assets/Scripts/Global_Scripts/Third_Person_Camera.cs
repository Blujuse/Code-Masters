using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Third_Person_Camera : MonoBehaviour
{
    [Header("Player References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    [Header("Camera References")]
    public CinemachineFreeLook freeLookCamera;
    public Slider sensitivitySlider;
    private static float SensValue = 0.5f;

    [Header("Camera Refrences")]
    public float RotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = player.GetComponent<Rigidbody>();

        sensitivitySlider.value = SensValue;
    }

    private void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * RotationSpeed);
        }

        SensValue = sensitivitySlider.value;
    }

    public void OnSensitivitySliderChanged()
    {
        float sensitivity = sensitivitySlider.value;
        freeLookCamera.m_XAxis.m_MaxSpeed = 4f * sensitivity;
        freeLookCamera.m_YAxis.m_MaxSpeed = 0.025f * sensitivity;
    }
}
