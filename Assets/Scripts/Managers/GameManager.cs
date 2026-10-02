using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerHealth playerHealth;

    [SerializeField]
    GameObject gameOverPanel;

    void OnEnable()
    {
        playerHealth.OnPlayerDied += HandlePlayerDied;
    }

    void OnDisable()
    {
        playerHealth.OnPlayerDied -= HandlePlayerDied;
    }

    private void HandlePlayerDied()
    {
        gameOverPanel.SetActive(true);
    }
}
