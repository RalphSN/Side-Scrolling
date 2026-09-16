using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage, (Vector2)transform.position);
        }
    }
}
