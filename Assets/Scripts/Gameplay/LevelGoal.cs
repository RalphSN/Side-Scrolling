using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelGoal : MonoBehaviour
{
    [SerializeField]
    private int requiredScore = 3;

    [SerializeField]
    ScoreManager scoreManager;
    private bool hasCompleted = false;
    public int RequiredScore
    {
        get { return requiredScore; }
    }
    public event Action OnCompleted;

    private bool CanComplete()
    {
        return scoreManager.Score >= requiredScore;
    }

    private void CompleteLevel()
    {
        hasCompleted = true;
        OnCompleted?.Invoke();
    }

    void OnTriggerStay2D(Collider2D other)
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
            CompleteLevel();
        }
    }
}
