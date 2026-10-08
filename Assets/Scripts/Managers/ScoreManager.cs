using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event Action<int> OnScoreChanged;
    private int score = 0;
    public int Score
    {
        get { return score; }
    }

    void OnEnable()
    {
        EnemyBase.OnEnemyDied += HandleEnemyDied;
    }

    void OnDisable()
    {
        EnemyBase.OnEnemyDied -= HandleEnemyDied;
    }

    private void HandleEnemyDied()
    {
        score++;
        OnScoreChanged?.Invoke(score);
    }
}
