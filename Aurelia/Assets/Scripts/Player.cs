using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    private BoxCollider2D playerCollider;

    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration = 70f;
    [SerializeField] private float maxMoveSpeed = 12f;
    [SerializeField] private float groundLinearDrag = 7f;
    private float horizontalDirection;
    private float verticalDirection;
    private bool changingDirection => (playerRB.velocity.x > 0f && horizontalDirection < 0f) || (playerRB.velocity.x < 0f && horizontalDirection > 0f);

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float hangTime = .1f;
    private float hangTimeCounter;
    private float jumpBufferCounter;
    bool isJumping = false;
    private bool canJump => jumpBufferCounter > 0f && hangTimeCounter > 0f;

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
        }
        
    }
       private void FixedUpdate()
    {
        playerRB.AddForce(new Vector2(horizontalDirection, 0f) * movementAcceleration);
        if (Mathf.Abs(playerRB.velocity.x) > maxMoveSpeed)
            playerRB.velocity = new Vector2(Mathf.Sign(playerRB.velocity.x) * maxMoveSpeed, playerRB.velocity.y);

        if (Mathf.Abs(horizontalDirection) < 0.4f || changingDirection)
            playerRB.drag = groundLinearDrag;
        else
            playerRB.drag = 0f;      
    }
 
    private Vector2 getInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }
}
