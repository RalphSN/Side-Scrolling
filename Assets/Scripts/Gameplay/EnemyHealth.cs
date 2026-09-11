using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 1;
    private int currentHp;
    private Animator animator;
    private bool isDead = false;

    void Awake()
    {
        currentHp = maxHp;
        animator = GetComponent<Animator>();
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
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("die");

        GetComponent<EnemyPatrol>().enabled = false;
        GetComponent<Collider2D>().isTrigger = true;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
        float deathAnimationLength = GetDeathAnimationLength();
        Destroy(gameObject, deathAnimationLength);
    }

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
}
