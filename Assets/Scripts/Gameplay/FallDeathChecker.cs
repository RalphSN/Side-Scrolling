using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class FallDeathChecker : MonoBehaviour
{
    [SerializeField]
    private Collider2D boundLine;
    private PlayerHealth playerHealth;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void FixedUpdate()
    {
        if (transform.position.y < boundLine.bounds.min.y - 5)
        {
            playerHealth.Die();
            enabled = false;
        }
    }
}
