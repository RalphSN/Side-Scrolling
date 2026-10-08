using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthText;

    [SerializeField]
    private TMP_Text killedText;

    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private LevelGoal levelGoal;

    [SerializeField]
    private ScoreManager scoreManager;

    void OnEnable()
    {
        playerHealth.OnHealthChanged += HandleHealthChanged;
        scoreManager.OnScoreChanged += HandleScoreChanged;
    }

    void Start()
    {
        HandleHealthChanged(playerHealth.CurrentHp, playerHealth.MaxHp);
        HandleScoreChanged(scoreManager.Score);
    }

    void OnDisable()
    {
        playerHealth.OnHealthChanged -= HandleHealthChanged;
        scoreManager.OnScoreChanged -= HandleScoreChanged;
    }

    private void HandleHealthChanged(int current, int max)
    {
        healthText.text = $"HP {current} / {max}";
    }

    private void HandleScoreChanged(int score)
    {
        killedText.text = $"Kill Score {score} / {levelGoal.RequiredScore}";
    }
}
