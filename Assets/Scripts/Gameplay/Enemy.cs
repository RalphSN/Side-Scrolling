using UnityEngine;

[RequireComponent(typeof(EnemyPatrol))]
public class Enemy : EnemyBase
{
    [SerializeField]
    private float hitStunDuration = 0.3f;
    private Coroutine hitStunCoroutine;
    private EnemyPatrol patrol;

    protected override void Awake()
    {
        base.Awake();
        patrol = GetComponent<EnemyPatrol>();
    }

    protected override void OnHurt()
    {
        base.OnHurt();
        if (hitStunCoroutine != null)
        {
            StopCoroutine(hitStunCoroutine);
        }
        hitStunCoroutine = StartCoroutine(HitStun());
    }

    private System.Collections.IEnumerator HitStun()
    {
        patrol.enabled = false;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(hitStunDuration);

        patrol.enabled = true;
        hitStunCoroutine = null;
    }

    protected override void OnDead()
    {
        base.OnDead();
        if (hitStunCoroutine != null)
        {
            StopCoroutine(hitStunCoroutine);
            hitStunCoroutine = null;
        } 
        patrol.enabled = false;
    }
}
