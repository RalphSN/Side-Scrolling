using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private Image healthFill;

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
        healthFill.fillAmount = (float)current / (float)max;
    }

    private void HandleScoreChanged(int score)
    {
        killedText.text = $"{score} / {levelGoal.RequiredScore}";
    }
}
