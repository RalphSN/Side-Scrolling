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

    [SerializeField]
    private GameObject fireballPrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float attackCooldown = 3f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    private PlayerControls controls;
    private Vector2 moveInput;
    private Vector3 initialScale;
    private int facingDirection = 1;
    private float lastAttackTime = -999f;

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
        if (Time.time - lastAttackTime < attackCooldown)
            return;
        lastAttackTime = Time.time;

        animator.SetTrigger("attack");

        GameObject fireballObj = Instantiate(
            fireballPrefab,
            firePoint.position,
            Quaternion.identity
        );
        Fireball fireball = fireballObj.GetComponent<Fireball>();
        fireball.SetDirection(facingDirection);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

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
}
