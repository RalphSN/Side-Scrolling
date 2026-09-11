using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;

    [SerializeField]
    private float lifeTime = 3f;
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
            collision.GetComponent<EnemyHealth>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
