using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    public int Score
    {
        get { return score; }
    }

    void OnEnable()
    {
        EnemyBase.OnEnemyDied += HandleAddScore;
    }

    void OnDisable()
    {
        EnemyBase.OnEnemyDied -= HandleAddScore;
    }

    private void HandleAddScore()
    {
        score++;
        Debug.Log($"現在分數{score}");
    }
}
