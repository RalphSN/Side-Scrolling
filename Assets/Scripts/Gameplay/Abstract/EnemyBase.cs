using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class EnemyBase : MonoBehaviour, IHazard, IDamageable
{
    [SerializeField]
    protected int maxHp = 1;

    [SerializeField]
    protected int damage = 1;

    [SerializeField]
    Collider2D standZone;
    public int Damage
    {
        get { return damage; }
    }
    protected int currentHp;
    public int MaxHp
    {
        get { return maxHp; }
    }
    public int CurrentHp
    {
        get { return currentHp; }
    }
    protected Animator animator;
    protected Rigidbody2D rb;
    protected bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
    }

    protected virtual void Awake()
    {
        currentHp = maxHp;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;
        currentHp -= amount;
        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("hurt");
            OnHurt();
        }
    }

    protected virtual void OnHurt() { }

    private void Die()
    {
        isDead = true;
        OnDead();
        animator.SetTrigger("die");
        enabled = false;
        
        if (standZone != null)
        {
            standZone.isTrigger = true;
        }

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
        float deathAnimationLength = GetDeathAnimationLength();
        Destroy(gameObject, deathAnimationLength);
    }

    protected virtual void OnDead() { }

    private float GetDeathAnimationLength()
    {
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        foreach (var clip in ac.animationClips)
        {
            if (clip.name.Contains("Death"))
                return clip.length;
        }
        return 1f;
    }

    public void OnPlayerContact(PlayerHealth player)
    {
        player?.TakeDamageWithKnockback(damage, (Vector2)transform.position);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!enabled)
            return;
        if (collision.CompareTag("Player"))
        {
            OnPlayerContact(collision.GetComponent<PlayerHealth>());
        }
    }
}
