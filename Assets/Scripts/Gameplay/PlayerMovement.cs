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
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    private PlayerControls controls;
    private Vector2 moveInput;
    private Vector3 initialScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
        animator = GetComponent<Animator>();
        initialScale = transform.localScale;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Move.performed += OnMove;
        controls.Gameplay.Move.canceled += OnMove;
        controls.Gameplay.Jump.performed += OnJump;
    }

    void OnDisable()
    {
        controls.Gameplay.Move.performed -= OnMove;
        controls.Gameplay.Move.canceled -= OnMove;
        controls.Gameplay.Jump.performed -= OnJump;
        controls.Gameplay.Disable();
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

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(initialScale.x),
                initialScale.y,
                initialScale.z
            );
        }
        else if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(initialScale.x),
                initialScale.y,
                initialScale.z
            );
        }

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        animator.SetBool("isRun", moveInput.x != 0f);
        animator.SetBool("isJump", !isGrounded);
    }
}
