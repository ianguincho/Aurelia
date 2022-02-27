using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D playerRB;
    public Animator playerAnimator;
    private BoxCollider2D playerCollider;

    public int side;
    [Header("Movement Variables")]
    //[SerializeField] private float movementAcceleration = 80f;
    [SerializeField] private float maxMoveSpeed = 25f;
    //[SerializeField] private float groundLinearDrag = 7f;
    private float xInput;
    private int direction = 1;

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
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();


        //Cursor.visible = false;
    }


    private void Update()
    {
        flip();
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

        //playerRB.AddForce(new Vector2(horizontalDirection, 0f) * movementAcceleration);
        //playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalDirection));


        //if (Mathf.Abs(horizontalDirection) < 0.4f)
        //    playerRB.drag = groundLinearDrag;
        //else
        //    playerRB.drag = 0f;
        Fall();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerAnimator.SetBool("IsJumping", false);
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    private void flip()
    {
        if (xInput < 0)
        {
            direction = -1;
            transform.localScale = new Vector3(direction, 1, 1);
        }
        if (xInput > 0)
        {
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
        xInput = context.ReadValue<Vector2>().x;
        playerAnimator.SetFloat("Speed", Mathf.Abs(xInput));
    }

    public void jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded())
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            playerAnimator.SetBool("IsJumping", true);

        }
        if (context.canceled && playerRB.velocity.y > 0f)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * .05f);
            playerAnimator.SetBool("IsJumping", true);
        }


    }
}

    
