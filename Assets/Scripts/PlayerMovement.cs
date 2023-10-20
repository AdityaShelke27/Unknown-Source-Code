using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float HorMov;
    public float VerMov;
    public Rigidbody rb;
    public float force;
    public bool isGrounded = true;
    public float stabalizer;

    //WallRunning Variables

    public LayerMask WhatIsWall;
    public float WallRunForce, maxWallRunTime, maxWallSpeed;
    public bool isWallRight, isWallLeft;
    public bool isWallRunning;
    
    public Transform PlayerCam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        

        
        Crouch();
        CheckForWall();
        WallRunInput();
    }

    private void FixedUpdate()
    {
        HorMov = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
        VerMov = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;
        rb.velocity = transform.forward * VerMov + transform.right * HorMov + new Vector3(0, rb.velocity.y, 0);
        
        if (HorMov == 0 && VerMov == 0 && rb.velocity.magnitude > 0 && isGrounded)
        {
            rb.AddForce(-(transform.forward * VerMov + transform.right * HorMov) * Time.fixedDeltaTime * stabalizer);
        }
        Jump();
    }

    public void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(transform.up * force * Time.fixedDeltaTime, ForceMode.Impulse);
        }
       
        if(isWallRunning)
        {
            //NormalJump
            if(Input.GetButtonDown("Jump"))
            {
                if (isWallLeft && HorMov <= 0 || isWallRight && HorMov >= 0)
                {
                    rb.AddForce(transform.up * force * Time.fixedDeltaTime);
                }
            }
            

            //Sidewards WallJump
            if(isWallRight && HorMov < 0)
            {
                rb.AddForce(-transform.right * WallRunForce * 4 * Time.fixedDeltaTime);
            }

            if (isWallLeft && HorMov > 0)
            {
                rb.AddForce(transform.right * WallRunForce * 4 * Time.fixedDeltaTime);
            }

            rb.AddForce((transform.forward + transform.up * 0.05f) * WallRunForce * 2 * Time.fixedDeltaTime);

        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 9)
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 9)
        {
            isGrounded = false;
        }
    }



    public void Crouch()
    {
        Vector3 PlayerScale = transform.localScale;
        if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            PlayerScale = new Vector3(PlayerScale.x, PlayerScale.y / 2, PlayerScale.z);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            PlayerScale = new Vector3(PlayerScale.x, PlayerScale.y * 2, PlayerScale.z);
        }
        transform.localScale = PlayerScale;
    }

    void WallRunInput()
    {
        if(HorMov > 0 && isWallRight)
        {
            StartWallRun();
        }

        if (HorMov < 0 && isWallLeft)
        {
            StartWallRun();
        }
    }

    void StartWallRun()
    {
        rb.useGravity = false;
        isWallRunning = true;
        if(rb.velocity.magnitude <= maxWallSpeed)
        {
            rb.AddForce(transform.forward * WallRunForce * Time.fixedDeltaTime);

            if(isWallRight)
            {
                rb.AddForce(transform.right * (WallRunForce / 5) * Time.fixedDeltaTime);
            }

            if(isWallLeft)
            {
                rb.AddForce(-transform.right * (WallRunForce / 5) * Time.fixedDeltaTime);
            }
        }
    }

    void StopWallRun()
    {
        rb.useGravity = true;
        isWallRunning = false;
    }

    void CheckForWall()
    {
        isWallRight = Physics.Raycast(transform.position, transform.right, 2f, WhatIsWall);
        isWallLeft = Physics.Raycast(transform.position, -transform.right, 2f, WhatIsWall);

        if(!isWallRight && !isWallLeft)
        {
            StopWallRun();
        }
    }    
}
