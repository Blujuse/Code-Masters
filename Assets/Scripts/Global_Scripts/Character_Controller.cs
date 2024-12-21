using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_Controller : MonoBehaviour
{
    // anything that says public I can adjust in editor
    [Header("Movement")]
    public float MoveSpeed;
    public float GroundDrag;

    [Header("Jumping")]
    public float JumpForce;
    public float JumpCooldown;
    public float AirMultiplier;
    public float FallSpeed;
    private float CoyoteCounter;
    public float CoyoteTime;
    bool ReadyToJump;

    [Header("Keybinds")]
    public KeyCode JumpKey = KeyCode.Space;

    // checks if player is on the ground, player height is used for the length of raycast
    [Header("Ground Check")]
    public float PlayerHeight;
    public LayerMask WhatisGround;
    public bool Grounded;

    // Slope stuff
    [Header("Slopes Handling")]
    public float MaxSlopeAngle;
    private RaycastHit SlopeHit;

    [Header("Player Objects")]
    public Transform Orientation;

    [Header("Private Variables")]
    float HorizontalInput;
    float VerticalInput;
    Vector3 MoveDirection;
    Rigidbody RB;

    [Header("Bools For Robot To Trigger")]
    public bool StopMoving = false;
    public bool PlayerInWinZone = false;

    [Header("Animation Bools")]
    public Animator PlayerAnim;

    [Header("Audio References")]
    public AudioSource SoundMaker;
    public AudioClip HoveringSound;
    public AudioSource SingleUseSoundMaker;
    public AudioClip JumpingSound;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.freezeRotation = true;

        ReadyToJump = true;
    }

    private void Update()
    {
        // ground check
        Grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.3f, WhatisGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (Grounded)
        {
            RB.drag = GroundDrag;
            CoyoteCounter = CoyoteTime;
        }
        else
        {
            RB.drag = 1f;
            CoyoteCounter -= Time.deltaTime;
        }

        if (RB.velocity.magnitude > 1 && Grounded)
        {
            PlayerAnim.SetBool("IsWalkingAnim", true);
            if (!SoundMaker.isPlaying)
            {
                SoundMaker.PlayOneShot(HoveringSound, 0.1f);
            }
        }
        else
        {
            PlayerAnim.SetBool("IsWalkingAnim", false);
            SoundMaker.Stop();
        }

        if (!Grounded)
        {
            PlayerAnim.SetBool("IsJumpingAnim", true);
        }
        else
        {
            PlayerAnim.SetBool("IsJumpingAnim", false);
        }
    }

    private void FixedUpdate()
    {        
        if (StopMoving == false)
        {
            MovePlayer();
        }
    }

    // checks for user input
    private void MyInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(JumpKey) && ReadyToJump && StopMoving == false && CoyoteCounter > 0f)
        {
            ReadyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), JumpCooldown);

            SingleUseSoundMaker.Stop();
            SingleUseSoundMaker.PlayOneShot(JumpingSound, .2f);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        MoveDirection = Orientation.forward * VerticalInput + Orientation.right * HorizontalInput;

        // on ground
        if (Grounded)
        {
            RB.AddForce(MoveDirection.normalized * MoveSpeed * 10f, ForceMode.Force);
        }
        else if (!Grounded)
        {
            RB.AddForce(MoveDirection.normalized * 10f * AirMultiplier, ForceMode.Force);
            RB.AddForce(Vector3.down * FallSpeed * RB.mass);
        }
            

        // on slope
        if (OnSlope())
        {
            RB.AddForce(GetSlopeMoveDirection() * MoveSpeed * 20f, ForceMode.Force);
        
            if(RB.velocity.y > 0)
            {
                RB.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        // turn gravity off when on slope
        RB.useGravity = !OnSlope();
    }

    // limits player velocity if it exceeds MoveSpeed variable
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(RB.velocity.x, 0f, RB.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > MoveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * MoveSpeed;
            RB.velocity = new Vector3(limitedVel.x, RB.velocity.y, limitedVel.z);
        }
    }

    // resets players vertical velocity and adds impulse force upwards
    private void Jump()
    {
        // reset y velocity
        RB.velocity = new Vector3(RB.velocity.x, 0f, RB.velocity.z);

        RB.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }
    
    // sets ReadyToJump bool back to true after period of time
    private void ResetJump()
    {
        ReadyToJump = true;
    }

    // what to do when player is on slope
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out SlopeHit, PlayerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, SlopeHit.normal);
            return angle < MaxSlopeAngle && angle != 0;
        }
    
        return false;
    }

    // This changes direction of force applied, rather than going into the slope it will go up it
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(MoveDirection, SlopeHit.normal).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillBox"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
