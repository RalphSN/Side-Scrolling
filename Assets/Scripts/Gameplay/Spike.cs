using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision
                .GetComponent<PlayerHealth>()
                .TakeDamageWithKnockback(damage, (Vector2)transform.position);
        }
    }
}
