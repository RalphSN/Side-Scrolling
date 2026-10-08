using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerHealth playerHealth;

    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    LevelGoal levelGoal;

    [SerializeField]
    GameObject stageClearPanel;

    void OnEnable()
    {
        playerHealth.OnPlayerDied += HandlePlayerDied;
        levelGoal.OnCompleted += HandleLevelCompleted;
    }

    void OnDisable()
    {
        playerHealth.OnPlayerDied -= HandlePlayerDied;
        levelGoal.OnCompleted -= HandleLevelCompleted;
    }

    private void HandlePlayerDied()
    {
        gameOverPanel.SetActive(true);
    }

    private void HandleLevelCompleted()
    {
        stageClearPanel.SetActive(true);
        playerMovement.Freeze();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
