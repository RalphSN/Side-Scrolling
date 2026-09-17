using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 1;

    [SerializeField]
    private float hitStunDuration = 0.3f;
    private Coroutine hitStunCoroutine;
    private int currentHp;
    private Animator animator;
    private Rigidbody2D rb;
    private EnemyPatrol patrol;
    private bool isDead = false;

    void Awake()
    {
        currentHp = maxHp;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        patrol = GetComponent<EnemyPatrol>();
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
            if (hitStunCoroutine != null)
            {
                StopCoroutine(hitStunCoroutine);
            }
            hitStunCoroutine = StartCoroutine(HitStun());
        }
    }

    private System.Collections.IEnumerator HitStun()
    {
        patrol.enabled = false;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(hitStunDuration);

        patrol.enabled = true;
        hitStunCoroutine = null;
    }

    private void Die()
    {
        isDead = true;

        if (hitStunCoroutine != null)
        {
            StopCoroutine(hitStunCoroutine);
            hitStunCoroutine = null;
        }

        animator.SetTrigger("die");
        GetComponent<Enemy>().enabled = false;
        GetComponent<Collider2D>().isTrigger = true;
        patrol.enabled = false;
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
