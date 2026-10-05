using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelGoal : MonoBehaviour
{
    private bool hasCompleted = false;
    public event Action OnCompleted;

    private bool CanComplete()
    {
        // TODO: 之後加上分數條件
        return true;
    }

    private void GameComplete()
    {
        hasCompleted = true;
        OnCompleted?.Invoke();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCompleted)
            return;
        if (!other.CompareTag("Player"))
            return;
        if (!CanComplete())
            return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null && !playerHealth.IsDead)
        {
            GameComplete();
        }
    }
}
