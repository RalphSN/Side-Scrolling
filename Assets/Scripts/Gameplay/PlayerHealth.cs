using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 10;

    [SerializeField]
    private float knockBackDuration = 0.3f;

    [SerializeField]
    private float knockBackForce = 0.5f;

    [SerializeField]
    private float hurtDuration = 1f;
    private float lastHurt = -999f;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private Coroutine knockBackCoroutine;
    private int currentHp;
    private Animator animator;
    private bool isGameover = false;

    void Awake()
    {
        currentHp = maxHp;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int amount, Vector2 damageSourcePosition)
    {
        if (isGameover)
            return;
        if (Time.time - lastHurt < hurtDuration)
            return;
        Vector2 direction = ((Vector2)transform.position - damageSourcePosition).normalized;
        currentHp -= amount;
        lastHurt = Time.time;
        if (currentHp <= 0)
        {
            // 結束遊戲
        }
        else
        {
            animator.SetTrigger("hurt");
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direction * knockBackForce, ForceMode2D.Impulse);
            if (knockBackCoroutine != null)
            {
                StopCoroutine(knockBackCoroutine);
            }
            knockBackCoroutine = StartCoroutine(knockBack());
        }
    }

    private System.Collections.IEnumerator knockBack()
    {
        playerMovement.enabled = false;
        animator.SetBool("isRun", false);
        animator.SetBool("isJump", false);
        yield return new WaitForSeconds(knockBackDuration);
        playerMovement.enabled = true;
        knockBackCoroutine = null;
    }
}
