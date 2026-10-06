using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int maxHp = 10;

    [SerializeField]
    private float knockBackDuration = 0.3f;

    [SerializeField]
    private float knockBackForce = 0.5f;

    [SerializeField]
    private float hurtDuration = 1f;

    [SerializeField]
    private LayerMask deadIgnoreLayers;
    public event Action OnPlayerDied;
    private float lastHurt = -999f;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerMovement playerMovement;
    private Coroutine knockBackCoroutine;
    private int currentHp;
    public int MaxHp
    {
        get { return maxHp; }
    }
    public int CurrentHp
    {
        get { return currentHp; }
    }

    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
    }

    void Awake()
    {
        currentHp = maxHp;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Die()
    {
        if (isDead)
            return;
        isDead = true;
        currentHp = 0;
        rb.excludeLayers |= deadIgnoreLayers;
        animator.SetTrigger("die");
        playerMovement.enabled = false;
        rb.linearVelocity = Vector2.zero;

        if (knockBackCoroutine != null)
        {
            StopCoroutine(knockBackCoroutine);
        }
        knockBackCoroutine = null;
        OnPlayerDied?.Invoke();
    }

    private bool ApplyDamage(int amount)
    {
        if (isDead)
            return false;
        if (Time.time - lastHurt < hurtDuration)
            return false;
        currentHp -= amount;
        lastHurt = Time.time;
        if (currentHp <= 0)
        {
            Die();
        }
        return true;
    }

    public void TakeDamage(int amount)
    {
        ApplyDamage(amount);
    }

    public void TakeDamageWithKnockback(int amount, Vector2 damageSourcePosition)
    {
        Vector2 direction = ((Vector2)transform.position - damageSourcePosition).normalized;
        bool hasHurt = ApplyDamage(amount);
        if (!isDead && hasHurt)
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
        yield return new WaitForSeconds(knockBackDuration);
        playerMovement.enabled = true;
        knockBackCoroutine = null;
    }
}
