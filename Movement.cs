using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    [Header("Developer tools")]
    [Space(10)]

    [SerializeField] bool flight = false;
    [SerializeField] bool speed = false;
    [SerializeField] bool lowGravity = false;
    [SerializeField] bool superJump = false;
    [SerializeField] bool lowFriction = false;
    

    [Header("Controllers")]
    [Space(10)]

    [SerializeField] Transform orientation;
    [SerializeField] Transform cam;
    [SerializeField] Transform player;
    [SerializeField] Transform groundCheck;
    [SerializeField] Rigidbody rb;
    [SerializeField] PhysicMaterial friction;
    [SerializeField] float gravity = 32f;
    [SerializeField] LayerMask collisionLayer;


    [Header("Player Variables")]
    [Space(10)]

    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float jumpHeight = 2;
    [SerializeField] float playerHeight = 4f;
   
    
    [Header("Movement Variables")]
    [Space(10)]

    [SerializeField] float distance;
    [SerializeField] float maxSpeed = 12f;
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float groundMultiplier = 2f;
    [SerializeField] float airDrag = 2f;
    [SerializeField] float airMultiplier = 0.4f;
    [SerializeField] float speedMultiplier = 10f;
    
    [Header("Sprinting")]
    [Space(10)]

    [SerializeField] float walkSpeed = 10f;
    [SerializeField] float sprintSpeed;
    [SerializeField] float acceleration = 10f;


    
    [Header("Keybinds")]
    [Space(5)]

    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    /*--- Private Variables ---*/
   
    float horizontalMovement;
    float verticalMovement;
    float groundSphere = 0.4f;
    float x, y;
    bool isGrounded;

    Vector3 MoveDirection;
    Vector3 slopeMoveDirection;

    
    

    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        return false;
    }


    // On Start
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }


    void Update()
    {
        DevTools();
        MoveInput();
        Gravity();
        ControlDrag();
        SpeedControl();
        Jump();

        isGrounded = Physics.CheckSphere(groundCheck.position, groundSphere, collisionLayer);      
    }

    void FixedUpdate()
    {
        Move();
    }

    /*--- Movement ---*/

    void MoveInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        MoveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

        slopeMoveDirection = Vector3.ProjectOnPlane(MoveDirection, slopeHit.normal);

        if (isGrounded && !OnSlope())
        {
            rb.AddForce(MoveDirection.normalized * speedMultiplier * playerSpeed, ForceMode.Acceleration);
        }
        
        else if (OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * speedMultiplier * playerSpeed, ForceMode.Acceleration);
        }
       
        else
        {
            rb.AddForce(MoveDirection.normalized * speedMultiplier * playerSpeed * airMultiplier, ForceMode.Acceleration);
        }
    }

    private Vector3 GroundNormals(float distance, LayerMask collisionLayer)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, distance, collisionLayer))
            return hit.normal;
        return Vector3.up;
    }



    void Jump()
    {
        if (isGrounded && Input.GetKey(jumpKey))
        {
            rb.AddForce(new Vector3(0, 1, 0) * jumpHeight, ForceMode.Impulse);
        }
    }


    void SpeedControl()
    {
        if (Input.GetKey(sprintKey))
        {
            sprintSpeed = walkSpeed * 1.8f;
            playerSpeed = Mathf.Lerp(playerSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }

        else
        {
            playerSpeed = Mathf.Lerp(playerSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    } 


    /*--- Physics ---*/
    void Gravity()
    {
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }

    void ControlDrag()
    {
        if(isGrounded)
        {
            rb.drag = groundDrag * groundMultiplier;
        }

        else
        {
            rb.drag = airDrag;
        }
    }

    /*--- Misc ---*/

    void DevTools()
    {
        if (speed == true)
        {
            playerSpeed = 100;
        }

        else if (flight == true)
        {
            rb.useGravity = false;
            gravity = 0f;
            MoveDirection = cam.forward * verticalMovement + cam.right * horizontalMovement;
        }

        else if (lowGravity == true)
        {
            gravity = 9;
            jumpHeight = 80;
        }

        else if (superJump == true)
        {
            jumpHeight = 200;
        }

        else if (lowFriction == true)
        {
            airDrag = 0f;
            groundDrag = 0f;
        }
    }
}
