using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D playerRB;
    public Animator playerAnimator;
    private BoxCollider2D playerCollider;

    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration = 80f;
    [SerializeField] private float maxMoveSpeed = 15f;
    [SerializeField] private float groundLinearDrag = 7f;
    private float horizontalDirection;
    private float verticalDirection;
    private int direction = 1;

    
    //We need to redo all the movement code so that we can allow for keybinding
    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 20f;


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
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalDirection = getInput().x;
        verticalDirection = getInput().y;
        //jump
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.velocity = Vector2.up * jumpForce;      
            playerAnimator.SetBool("IsJumping", true);
        }

        //dash                        
    }
    private void FixedUpdate()
    {
        flip();
        playerRB.AddForce(new Vector2(horizontalDirection, 0f) * movementAcceleration);
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalDirection));
        if (Mathf.Abs(playerRB.velocity.x) > maxMoveSpeed)
        {
            playerRB.velocity = new Vector2(Mathf.Sign(playerRB.velocity.x) * maxMoveSpeed, playerRB.velocity.y);
        }

        if (Mathf.Abs(horizontalDirection) < 0.4f)
            playerRB.drag = groundLinearDrag;
        else
            playerRB.drag = 0f;

        Fall();
    }
 
    private Vector2 getInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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
        if (horizontalDirection < 0)
        {
            direction = -1;
            transform.localScale = new Vector3(direction, 1, 1);
        }
        if (horizontalDirection > 0)
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
}
