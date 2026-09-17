using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!enabled) return; 
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage, (Vector2)transform.position);
            Debug.Log(
                $"呼叫者 EntityId: {GetEntityId()}, 這個物件身上共有 {GetComponents<Enemy>().Length} 份 Enemy 元件, enabled = {this.enabled}"
            );
        }
    }
}
