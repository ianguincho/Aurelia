using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public PlayerControls controls;
    InputAction climbAction;
    [Header("Components")]
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D playerRB;
    public Animator playerAnimator;
    public GhostAnimationForDash ghost;
    private BoxCollider2D playerCollider;
    private Collision coll;

    public int side;
    [Header("Movement Variables")]
    //[SerializeField] private float movementAcceleration = 80f;
    [SerializeField] private float maxMoveSpeed = 25f;
    //[SerializeField] private float groundLinearDrag = 7f;
    private float xInput;
    private int direction = 1;
    
    //wallslide and walljump variables
    bool wallSlide;
    public float wallSlideSpeed;
    bool wallJump;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    [Header("Climb")]
    public float distance;
    public LayerMask whatIsLadder;
    private bool isClimbing;
    private float inputHorizontal;
    private float inputVertical;
    private float climbSpeed = 30f;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 30f;
    [SerializeField] private float hangTime = 0.1f;
    private float hangTimeCounter;

    [Header("Dash")]
    [SerializeField] public float dashDistnace = 30f;
    bool isDashing;

    public ParticleSystem dust;
    public Transform dustScale;
    public ParticleSystem dustLand;


    //[Header("Dash Variables")]
    //public float dashSpeed;
    //private float dashTime;
    //public float startDashTime;
    //private int dashDirection;

    [Header("Singleton Instantiation")]
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<Player>();
            return instance;
        }
    }



    // Start is called before the first frame update
    private void Start()
    {
        coll = GetComponent<Collision>();
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        controls = new PlayerControls();
        controls.Enable();
        climbAction = controls.PlayerController.Climb;
        //Cursor.visible = false;
    }


    private void Update()
    {
        //WallJump and WallSlide
        float input = Input.GetAxisRaw("Horizontal");
        if(coll.onWall == true && !coll.onGround && input != 0)
        {
            playerAnimator.SetBool("IsClimbing", true);
            wallSlide = true;
        }
        else
        {
            playerAnimator.SetBool("IsClimbing", false);
            wallSlide = false;
        }
        if(wallSlide == true)
        {
            playerAnimator.SetBool("IsClimbing", true);
            playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Clamp(playerRB.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        if (climbAction.triggered && wallSlide == true)
        {
            playerAnimator.SetBool("IsClimbing", true);
            wallJump = true;
            Invoke("SetWallJumpToFalse", wallJumpTime);
        }
        if(wallJump == true)
        {
            playerRB.velocity = new Vector2(xWallForce * -input, yWallForce);
        }
        //end of new code

        
        if(!isDashing)
            playerRB.velocity = new Vector2(xInput * maxMoveSpeed, playerRB.velocity.y);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, distance, whatIsLadder);
        if (hitInfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                isClimbing = true;
            }
        }
        else
        {
            isClimbing = false;
        }
        if (isClimbing == true)
        {
            playerAnimator.SetBool("IsClimbing", true);
            inputVertical = Input.GetAxisRaw("Vertical");
            playerRB.velocity = new Vector2(playerRB.velocity.x, inputVertical * climbSpeed);
            playerRB.gravityScale = 0;
        }
        else
        {
            playerRB.gravityScale = 5;
        }
    }
    private void FixedUpdate()
    {    
        Fall();
    }

    private void SetWallJumpToFalse()
    {
        wallJump = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        CreateDustLand();
        playerAnimator.SetBool("IsJumping", false);
        playerAnimator.SetBool("IsClimbing", false);
    }
    private bool isGrounded()
    {
        //return Physics2D.OverlapCircle()
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    private void flip()
    {
        CreateDust();
        if (xInput < 0)
        {
            dustScale.localScale = new Vector3(-1, 1, 1);
            direction = -1;
            transform.localScale = new Vector3(direction, 1, 1);
        }
        if (xInput > 0)
        {
            dustScale.localScale = new Vector3(1, 1, 1);
            direction = 1;
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }
    public void Fall()
    {
        if (playerRB.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps"))
        {
            FindObjectOfType<GameManager>().Restart();
        }
    }

    public void movement(InputAction.CallbackContext context)
    {
        flip();
        if (!isDashing)
        {
            
            xInput = context.ReadValue<Vector2>().x;
            playerAnimator.SetFloat("Speed", Mathf.Abs(xInput));
        }
    }

    public void jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded())
        {
            playerAnimator.SetBool("IsJumping", true);
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);

        }
        if (context.canceled && playerRB.velocity.y > 0f)
        {
            playerAnimator.SetBool("IsJumping", true);
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * .05f);
        }


    }
    public void Dash(InputAction.CallbackContext context)
    {
        Debug.Log(xInput);
        //Dashing left
        if (xInput == -1)
        {
            StartCoroutine(dash(-1f));
        }
        else if (xInput == 0)
        {
            StartCoroutine(dash(1f));
        }
        else if (xInput == 1)
        {
            StartCoroutine(dash(1f));
        }

    }
    public void Climb(InputAction.CallbackContext context)
    {
        Debug.Log("Action Pressed");
        
    }
    IEnumerator dash(float direction)
    {
        isDashing = true;
        ghost.makeGhost = true;
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
        playerRB.AddForce(new Vector2(dashDistnace * direction, 0f), ForceMode2D.Impulse);
        float gravity = playerRB.gravityScale;
        yield return new WaitForSeconds(1f);
        isDashing = false;
        ghost.makeGhost = false;
        playerRB.gravityScale = gravity;
    }

    void CreateDust()
    {
        dust.Play();
    }
    void CreateDustLand()
    {
        dustLand.Play();
    }
}

    
