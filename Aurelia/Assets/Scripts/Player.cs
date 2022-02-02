using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D playerRB;
    private Animator playerAnimator;

    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration = 70f;
    [SerializeField] private float maxMoveSpeed = 12f;
    [SerializeField] private float groundLinearDrag = 7f;
    private float horizontalDirection;
    private float verticalDirection;
    private bool changingDirection => (playerRB.velocity.x > 0f && horizontalDirection < 0f) || (playerRB.velocity.x < 0f && horizontalDirection > 0f);

    [Header("Jump Variables")]
    [SerializeField] private float airLinearDrag = 2.5f;

    // Start is called before the first frame update
    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalDirection = getInput().x;
        verticalDirection = getInput().y;
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


}
