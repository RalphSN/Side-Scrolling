using UnityEngine;

public class Spike : MonoBehaviour, IHazard
{
    [SerializeField]
    private int damage = 1;
    public int Damage
    {
        get { return damage; }
    }

    public void OnPlayerContact(PlayerHealth player)
    {
        player?.TakeDamageWithKnockback(damage, (Vector2)transform.position);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerContact(collision.GetComponent<PlayerHealth>());
        }
    }
}
