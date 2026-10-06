using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float jumpForce = 8f;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundCheckRadius = 0.1f;

    [SerializeField]
    private LayerMask groundLayer;
    private PlayerAttack playerAttack;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    private PlayerControls controls;
    private Vector2 moveInput;
    private Vector3 initialScale;
    private int facingDirection = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
        playerAttack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
        initialScale = transform.localScale;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Move.performed += OnMove;
        controls.Gameplay.Move.canceled += OnMove;
        controls.Gameplay.Jump.performed += OnJump;
        controls.Gameplay.Attack.performed += OnAttack;
        moveInput = Vector2.zero;
    }

    void OnDisable()
    {
        controls.Gameplay.Move.performed -= OnMove;
        controls.Gameplay.Move.canceled -= OnMove;
        controls.Gameplay.Jump.performed -= OnJump;
        controls.Gameplay.Attack.performed -= OnAttack;
        controls.Gameplay.Disable();
        animator.SetBool("isRun", false);
        animator.SetBool("isJump", false);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        playerAttack.Attack(facingDirection);
    }

    void FixedUpdate()
    {
        isGrounded =
            Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer)
            && rb.linearVelocity.y <= 0.1f;
        // if(isGrounded != wasGroundedLastFrame)
        // {
        //     Debug.Log($"[{Time.time:F3}] isGrounded 變成 {isGrounded}");
        //     wasGroundedLastFrame = isGrounded;
        // }

        if (moveInput.x < 0)
        {
            facingDirection = -1;
        }
        else if (moveInput.x > 0)
        {
            facingDirection = 1;
        }
        transform.localScale = new Vector3(
            facingDirection * Mathf.Abs(initialScale.x),
            initialScale.y,
            initialScale.z
        );

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        animator.SetBool("isRun", moveInput.x != 0f);
        animator.SetBool("isJump", !isGrounded);
    }

    public void Freeze()
    {
        rb.linearVelocity = Vector2.zero;
        enabled = false;
    }
}
