using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        UpdateAnimations();
        CheckLanding();
    }

    void Move()
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
            spriteRenderer.flipX = true;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.W)&& IsGrounded()))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
        }
    }

    bool IsGrounded()
    {
        Vector2 origin = col.bounds.center;
        Vector2 size = col.size;
        float castDistance = 0.1f;

        RaycastHit2D hit = Physics2D.CapsuleCast(origin, size, CapsuleDirection2D.Vertical, 0f, Vector2.down, castDistance, groundLayer);
        Debug.DrawLine(origin, origin + Vector2.down * (col.bounds.extents.y + castDistance), Color.green);

        return hit.collider != null;
    }



    void UpdateAnimations()
    {
        float speed = Mathf.Abs(rb.linearVelocity.x);
        float verticalVelocity = rb.linearVelocity.y;
        bool grounded = IsGrounded();

        animator.SetFloat("Speed", speed);
        animator.SetBool("IsGrounded", grounded);
        animator.SetBool("IsJumping", isJumping && verticalVelocity > 0f); 
        animator.SetFloat("VerticalVelocity", verticalVelocity);
    }

    void CheckLanding()
    {
        if (isJumping && IsGrounded() && rb.linearVelocity.y <= 0f)
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }
}
