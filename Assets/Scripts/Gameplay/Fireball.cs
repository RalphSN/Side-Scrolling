using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;

    [SerializeField]
    private float lifeTime = 3f;

    [SerializeField]
    private GameObject hitEffectPrefab;
    private Rigidbody2D rb;
    private int direction = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(int newDirection)
    {
        direction = newDirection;
        transform.rotation = Quaternion.Euler(0, 0, direction == 1 ? 90f : -90f);
        rb.linearVelocity = new Vector2(direction * speed, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth.IsDead)
                return;
            enemyHealth.TakeDamage(1);

            if (hitEffectPrefab != null)
            {
                Vector3 hitPosition = collision.transform.position + new Vector3(0, 2f, 0);
                GameObject effect = Instantiate(hitEffectPrefab, hitPosition, Quaternion.identity);
                Destroy(effect, 1.5f);
            }

            Destroy(gameObject);
        }
    }
}
