using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerHealth playerHealth;

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    LevelGoal levelGoal;

    [SerializeField]
    GameObject stageClearPanel;

    void OnEnable()
    {
        playerHealth.OnPlayerDied += HandlePlayerDied;
        levelGoal.OnCompleted += HandlePlayerWin;
    }

    void OnDisable()
    {
        playerHealth.OnPlayerDied -= HandlePlayerDied;
        levelGoal.OnCompleted -= HandlePlayerWin;
    }

    private void HandlePlayerDied()
    {
        gameOverPanel.SetActive(true);
    }

    private void HandlePlayerWin()
    {
        stageClearPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
